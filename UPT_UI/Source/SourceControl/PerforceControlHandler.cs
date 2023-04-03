using Perforce.P4;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;

namespace UPT_UI.Source.SourceControl
{
    public class PerforceControlHandler : ISourceControlHandler
    {
        public void Connect(string InURL, string InUser, string InWorkspace, string InPassword /*= ""*/)
        {
            Perforce.P4.Server Server = new Perforce.P4.Server(new Perforce.P4.ServerAddress(InURL));
            Repo = new Perforce.P4.Repository(Server);
            Connection = Repo.Connection;

            Connection.UserName = InUser;
            Connection.Client = new Perforce.P4.Client();
            Connection.Client.Name = InWorkspace;

            try
            {
                if(Connection.Connect(null))
                {
                    Perforce.P4.Credential Cred = Connection.Login(InPassword);

                    if (Cred.Ticket != "")
                    {
                        MainWindow.ReportProgress("Connecting to P4Server: " + InURL);
                        MainWindow.ReportProgress("User: " + InUser);
                        MainWindow.ReportProgress("Workspace: " + InWorkspace);
                        MainWindow.ReportProgress("Ticket expires " + Cred.Expires.ToString());
                    };
                }
                else
                {
                    MessageBox.Show("Not connected");
                }
            }
            catch(Perforce.P4.P4Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public bool CheckoutFiles(List<string> InFiles, string InCLName /*= "" */)
        {
            if (IsInitialised())
            {
                List<Perforce.P4.FileSpec> EditSpecs = new List<Perforce.P4.FileSpec>();
                List<Perforce.P4.FileSpec> AddSpecs = new List<Perforce.P4.FileSpec>();

                foreach (string File in InFiles)
                {
                    FileInfo Info = new FileInfo(File);
                    Perforce.P4.FileSpec NewFileSpec = new Perforce.P4.FileSpec(new Perforce.P4.DepotPath(File));
                        //THIS CAN BE MADE TIGHER, IM SURE OF IT
                        IList<Perforce.P4.FileSpec> DepotFile = Connection.Client.GetSyncedFiles(null, NewFileSpec);

                    if(DepotFile == null)
                    {
                        AddSpecs.Add(NewFileSpec);
                    }
                    else
                    {
                        FileSpec fs = new FileSpec(new DepotPath(File), null);
                        IList<FileSpec> lfs = new List<FileSpec>();
                        lfs.Add(fs);

                        GetFileMetaDataCmdOptions FileMetaDataOptions = new GetFileMetaDataCmdOptions(GetFileMetadataCmdFlags.None, null, null, 0, null, null, null);
                        IList<FileMetaData> target = Repo.GetFileMetaData(lfs, FileMetaDataOptions);
                        if(target.Count > 0)
                        {
                            //File is already checked out somewhere - add a message box to move the file maybe?
                            if(target[0].Change == -1)
                            {
                                EditSpecs.Add(NewFileSpec);
                            }
                        }
                    }
                }

                if (EditSpecs.Count == 0 && AddSpecs.Count == 0)
                {
                    //All files we want to play with are already read only or otherwise skipped
                    return true;
                }

                ChangesCmdOptions opts = new ChangesCmdOptions(ChangesCmdFlags.None, null, 0, ChangeListStatus.Pending, Connection.UserName);
                IList<Changelist> CLs = Repo.GetChangelists(opts, null);

                string CLDesc = InCLName == "" ? "UPT Generated CL" : InCLName;

                Changelist CLToUse = null;

                if (CLs != null)
                {
                    foreach (Changelist CL in CLs)
                    {
                        IList<FileMetaData> CLFiles = CL.Files;
                        if (CL.Description == CLDesc)
                        {
                            CLToUse = CL;
                            break;
                        }
                    }
                }

                if (CLToUse == null)
                {
                    Changelist NewChangelist = new Perforce.P4.Changelist();
                    NewChangelist.Type = ChangeListType.Public;
                    NewChangelist.Description = CLDesc;
                    CLToUse = Repo.CreateChangelist(NewChangelist);
                }

                Perforce.P4.Options EditOptions = new Perforce.P4.Options();
                EditOptions["-c"] = CLToUse.Id.ToString();

                if (EditSpecs.Count > 0)
                {
                    IList<Perforce.P4.FileSpec> EditedFiles = Connection.Client.EditFiles(EditOptions, EditSpecs.ToArray());
                }

                if(AddSpecs.Count > 0)
                {
                    IList<Perforce.P4.FileSpec> AddedFiles = Connection.Client.AddFiles(EditOptions, AddSpecs.ToArray());
                }
              
                return true;
            }

            return false;
        }

        public bool IsInitialised()
        {
            if(Connection == null)
            {
                return false;
            }

            return Connection.Status == Perforce.P4.ConnectionStatus.Connected;
        }

        Perforce.P4.Repository Repo;
        Perforce.P4.Connection Connection;
    }
}