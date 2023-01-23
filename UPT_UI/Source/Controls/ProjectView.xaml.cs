using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace UPT_UI.Controls
{
    public partial class ProjectView : UserControl
    {
        public ProjectView()
        {
            InitializeComponent();

            Loaded += OnWindowLoaded;
        }

        private void OnWindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                MainWindow.ProjectWorker.OnProjectInitialisedDel += OnProjectInitialisedDel;
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
        }

        private void OnProjectInitialisedDel(bool wassuccessful)
        {
            MainWindow.SyncContext.Post(_ =>
            {
                ProjectNameText.Text = "Project Name: " + MainWindow.ProjectWorker.GetProjectName();
                CopyrightText.Text = "Copyright Notice: " + MainWindow.ProjectWorker.DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");
                ProjectVersionText.Text = "Project Version: " + MainWindow.ProjectWorker.DefaultGameConfigReader.GetValForKey(@"ProjectVersion");
            }, null);
        }

        private void FixCopyrightButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowOutput(true);
            
            Task.Factory.StartNew(() => { StartSourceScan(); });
        }

        private void StartSourceScan()
        {
            string[] Files = Directory.GetFiles(MainWindow.ProjectWorker.SourceDirectory, "*.*", SearchOption.AllDirectories);
            Dictionary<string, string> InvalidFiles = new Dictionary<string, string>();
            List<string> InvalidFileNames = new List<string>();
            int FileNum = 1;

            foreach (string File in Files)
            {
                string IncorrectFirstLine = "";
                if (CheckFileHasCorrectCopyright(File, out IncorrectFirstLine))
                {
                    MainWindow.ReportProgress("[Copyright Check - PASS] " + File);
                }
                else
                {
                    MainWindow.ReportProgress("[Copyright Check - FAIL] " + File);
                    InvalidFiles.Add(File, IncorrectFirstLine);
                    InvalidFileNames.Add(File);
                }
                
                FileNum++;
            }

            if (InvalidFiles.Count > 0)
            {
                if (MainWindow.SourceControlHandler.IsInitialised())
                {
                    MainWindow.SourceControlHandler.CheckoutFiles(InvalidFileNames, "[UPT] Copyright Fixup");
                }

                TryApplyCopyrightToFiles(InvalidFiles);
            }
            else
            {
                MainWindow.ReportProgress("Project is copyright compliant");
            }
        }

        private void TryApplyCopyrightToFiles(Dictionary<string, string> InFiles)
        {
            string NewCopyrightLine = "// " + MainWindow.ProjectWorker.DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");

            int NumFixes = 0;
            foreach(KeyValuePair<string, string> FilePair in InFiles)
            {
                string CurrentFirstLine = FilePair.Value;
                string[] FileCopy = File.ReadAllLines(FilePair.Key);

                MainWindow.ReportProgress(FilePair.Key);
                MainWindow.ReportProgress(FilePair.Value);
                    
                if (CurrentFirstLine.Contains(@"//"))
                {
                    MainWindow.ReportProgress("First line is a comment");

                    FileCopy[0] = NewCopyrightLine;

                    if(FileCopy[1] != "")
                    {
                        List<string> CopyAsList = new List<string>();
                        CopyAsList = FileCopy.ToList<string>();
                        CopyAsList.Insert(1, ""); //Create a new line

                        FileCopy = CopyAsList.ToArray();
                    }

                    WriteToFile(FilePair.Key, FileCopy);
                    NumFixes++;
                }
                else if (CurrentFirstLine.Contains(@"#") || CurrentFirstLine.Contains(@"using"))
                {
                   MainWindow.ReportProgress("First line is an include or a pragma, or is importing an external library for a build or target file");

                    List<string> CopyAsList = new List<string>();
                    CopyAsList = FileCopy.ToList<string>();

                    CopyAsList.Insert(0, "");
                    CopyAsList.Insert(0, NewCopyrightLine);

                    FileCopy = CopyAsList.ToArray();

                    WriteToFile(FilePair.Key, FileCopy);
                    NumFixes++;
                }
                else
                {
                    MainWindow.ReportProgress("File " + FilePair.Key + "has an unknown usecase - not altering anything for the time being");
                }
            }

           MainWindow.ReportProgress("Fixed up " + NumFixes + " files");
        }
        
        private void WriteToFile(string InPath, string[] NewFileText)
        {
            System.IO.File.WriteAllLines(InPath, NewFileText);
        }
        
        private bool CheckFileHasCorrectCopyright(string InFile, out string IncorrectFirstLine) 
        {
            string FirstLine = File.ReadLines(InFile).First();
            string CopyrightNotice = MainWindow.ProjectWorker.DefaultGameConfigReader.GetValForKey(@"CopyrightNotice");

            if (FirstLine != ("// " + CopyrightNotice))
            {
                IncorrectFirstLine = FirstLine;
                return false;
            }

            IncorrectFirstLine = "";
            return true;
        }
        private void DeleteEmptyDirectoryButton_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.ShowOutput(true);
            CheckDirectoryIsEmpty(MainWindow.ProjectWorker.SourceDirectory);
        }
        private void CheckDirectoryIsEmpty(string InDirectory)
        {
            foreach (string Dir in Directory.GetDirectories(InDirectory))
            {
                CheckDirectoryIsEmpty(Dir);
                if (Directory.GetFiles(Dir).Length == 0 &&
                    Directory.GetDirectories(Dir).Length == 0)
                {
                    Directory.Delete(Dir, false);
                    MainWindow.ReportProgress("[Directory Check - FAILED] Directory " + Dir + " is empty deleting");
                }
                else
                {
                    MainWindow.ReportProgress("[Directory Check - PASSED] Directory " + Dir + " is not empty - continuing");
                }
            }
        }
    }
}