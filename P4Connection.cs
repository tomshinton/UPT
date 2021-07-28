using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;
using Perforce.P4;

using UnrealProjectTool.Properties;

namespace UnrealProjectTool
{
    public partial class P4Connection : Form
    {
        public delegate void OnP4ConnectedDelegate(Perforce.P4.Repository Repo);
        public OnP4ConnectedDelegate OnP4Connected;
        private bool ValidConnectionInfo;
        public P4Connection()
        {
            InitializeComponent();
        }

        private void P4Connection_Load(object sender, EventArgs e)
        {

        }
        private void ConnectButton_Click(object sender, EventArgs e)
        {
            string P4Server = ServerBox.Text;
            string P4Workspace = WorkspaceBox.Text;
            string P4User = UserBox.Text;

            Console.WriteLine("Attempting connection...");

            Server Svr = new Server(new ServerAddress(P4Server));
            Repository Repo = new Repository(Svr);

            Repo.Connection.UserName = P4User;
            ClientsCmdOptions opts = new ClientsCmdOptions(ClientsCmdFlags.IgnoreCase, null, null, 10, null);

            Repo.Connection.SetClient(P4Workspace);

            Connect(Repo, false);
        }

        public bool Connect(Repository InRepo, bool IsFromPersistance)
        {
            try
            {
                InRepo.Connection.Connect(new Options());
                InRepo.Connection.Login(PasswordBox.Text);

                P4Command Command = new P4Command(InRepo, "info", true, null);
                P4CommandResult Result = Command.Run();

                if (Result.Success)
                {
                    if(!IsFromPersistance)
                    {
                        SaveToSettings();                                
                    }
                    
                    OnP4Connected.Invoke(InRepo);

                    this.Close();
                    MessageBox.Show("Successfully connected to Perforce", "Success");

                    return true;
                }
            }
            catch (P4Exception Ex)
            {
                if(Ex.Message.Contains("Password"))
                {
                    string EnteredPassword = MainForm.ShowDialog("Enter Password: ", "");

                    if(EnteredPassword != "")
                    {
                        PasswordBox.Text = EnteredPassword;
                        Connect(InRepo, IsFromPersistance);
                    }
                }
                else
                {
                    MessageBox.Show(Ex.Message, "Failure", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return false;
        }
        private void LoginInfoChanged(object sender, EventArgs e)
        {
            ValidConnectionInfo = UserBox.Text != "" && WorkspaceBox.Text != "" && ServerBox.Text != "";
            ConnectButton.Enabled = ValidConnectionInfo;
        }

        private void SaveToSettings()
        {
            if (MainForm.UsePersistance)
            {
                Settings.Default.LastP4Server = ServerBox.Text;
                Settings.Default.LastP4Workspace = WorkspaceBox.Text;
                Settings.Default.LastP4User = UserBox.Text;

                Settings.Default.Save();
            }
        }

        public bool TryConnectFromPersistance()
        {
            if(HasSavedP4Data())
            {
                Console.WriteLine("Attempting connection from persistent data...");

                Server Svr = new Server(new ServerAddress(Settings.Default.LastP4Server));
                Repository Repo = new Repository(Svr);

                Repo.Connection.UserName = Settings.Default.LastP4User;
                ClientsCmdOptions opts = new ClientsCmdOptions(ClientsCmdFlags.IgnoreCase, null, null, 10, null);

                Repo.Connection.SetClient(Settings.Default.LastP4Workspace);

                return Connect(Repo, true);
            }

            return false;
        }

        private bool HasSavedP4Data()
        {
            return Settings.Default.LastP4Server != "" && Settings.Default.LastP4User != "" && Settings.Default.LastP4Workspace != "";
        }
    }
}
