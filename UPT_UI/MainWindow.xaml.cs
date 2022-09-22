using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Microsoft.WindowsAPICodePack.Dialogs;

using UPT_UI.Controls;
using UPT_UI.Source.SourceControl;
using UPT_UI.Windows;

namespace UPT_UI
{
    public partial class MainWindow : Window
    {
        public static UProjectWorker ProjectWorker;

        //Make these dynamic when more API support is added - starting with GDrive and P4 for now
        public static IAuthHandler CurrAuthHandler = new GoogleAuthHandler();
        public static ISourceControlHandler SourceControlHandler = new PerforceControlHandler();

        static SynchronizationContext SyncContext;
        public static MainWindow Instance;

        public MainWindow()
        {
            InitializeComponent();

            Instance = this;
            SyncContext = SynchronizationContext.Current;

            CurrAuthHandler.RegisterAuthorisationCallback(OnCredentialsAuthorised);

            string LastProjectDir = Properties.Settings.Default.LastProjectDir;
            string LastEngineDir = Properties.Settings.Default.LastEngineDir;
            if (LastProjectDir != "" && File.Exists(LastProjectDir))
            {
                if (LastEngineDir != "" && Directory.Exists(LastEngineDir))
                {
                    InitialiseProjectFromString(LastProjectDir, LastEngineDir);
                }
            }
        }

        private void MinimizeButton_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void ExitButton_Click(object sender, RoutedEventArgs e)
        {
            Properties.Settings.Default.Save();
            System.Diagnostics.Process.GetCurrentProcess().Kill();
        }

        private void Toolbar_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            if (e.LeftButton == System.Windows.Input.MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BindProjectButton_Click(object sender, RoutedEventArgs e)
        {
            CommonOpenFileDialog FindProjectDialog = new CommonOpenFileDialog();

            string InitialDirectory = Properties.Settings.Default.LastProjectDir != "" ? Directory.GetParent(Properties.Settings.Default.LastProjectDir).ToString() : @"C:\";

            FindProjectDialog.Title = "Browse for a uProject file to bind to";
            FindProjectDialog.InitialDirectory = InitialDirectory;
            FindProjectDialog.DefaultExtension = ".uproject";
            CommonFileDialogFilter Filter = new CommonFileDialogFilter("Unreal Project Files(*.uproject)", "*uproject");
            FindProjectDialog.Filters.Add(Filter);

            if (FindProjectDialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                string BoundProjectDir = FindProjectDialog.FileName;

                CommonOpenFileDialog FindEngineDialog = new CommonOpenFileDialog();
                FindEngineDialog.Title = "Browse to this project's engine directory";

                string EngineInitialDirectory = Properties.Settings.Default.LastEngineDir != "" ? Directory.GetParent(Properties.Settings.Default.LastEngineDir).ToString() : @"C:\";
                FindEngineDialog.InitialDirectory = EngineInitialDirectory;
                FindEngineDialog.IsFolderPicker = true;

                if (FindEngineDialog.ShowDialog() == CommonFileDialogResult.Ok)
                {
                    string BoundEngineDirectory = FindEngineDialog.FileName;

                    if (BoundProjectDir != "" && BoundEngineDirectory != "")
                    {
                        InitialiseProjectFromString(BoundProjectDir, BoundEngineDirectory);
                    }
                }
            }
        }

        private void InitialiseProjectFromString(string InProjectDir, string InEngineDirectory)
        {
            MainWindow.ProjectWorker = new UProjectWorker(InProjectDir, InEngineDirectory);

            //Called from other thread - any resulting function calls need to be synced
            MainWindow.ProjectWorker.OnProjectInitialisedDel += OnProjectInitialised;
        }

        private void OnProjectInitialised()
        {
            SyncContext.Post(_ =>
            {
                UnrealProjectToolTitle.Text = "UPT - " + ProjectWorker.GetProjectName();
                Workspace.IsEnabled = true;

                BuildModulePanel();

                //BuildProjectInfoPanel();
            }, null);
        }

        private void DeployButton_Click(object sender, RoutedEventArgs e)
        {
            ProjectWorker.ClearDeployQueue();

            if(ProjectWorker.SupportsTarget(TargetType.Client))
            {
                ProjectWorker.DeployClient();
            }

            if(ProjectWorker.SupportsTarget(TargetType.Server))
            {
                ProjectWorker.DeployServer();
            }

            ProjectWorker.ProcessDeployQueue();
        }

        private void AccountButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.CurrAuthHandler.Login();
        }

        private void OnCredentialsAuthorised()
        {
            if (MainWindow.CurrAuthHandler.IsAuthorised())
            {
                MainWindow.CurrAuthHandler.SetUserPhoto(AccountCoverPhoto);
            }
        }

        private void BuildOutlogLogScrollViewer_ScrollChanged(object sender, System.Windows.Controls.ScrollChangedEventArgs e)
        {
            bool Autoscroll = true;
            if (e.ExtentHeightChange == 0)
            { 
                if (BuildOutlogLogScrollViewer.VerticalOffset == BuildOutlogLogScrollViewer.ScrollableHeight)
                {   
                    Autoscroll = true;
                }
                else
                {   
                    Autoscroll = false;
                }
            }

            if (Autoscroll && e.ExtentHeightChange != 0)
            {   
                BuildOutlogLogScrollViewer.ScrollToVerticalOffset(BuildOutlogLogScrollViewer.ExtentHeight);
            }
        }

        public static void ClearProgress()
        {
            TextBlock FoundBlock = (TextBlock)Instance.FindName("BuildOutputLog");

            if (FoundBlock != null)
            {
                FoundBlock.Text = "";
            }
        }

        public static void ReportProgress(string InReport)
        {
            MainWindow.SyncContext.Post(_ =>
            {
                TextBlock FoundBlock = (TextBlock)Instance.FindName("BuildOutputLog");

                if (FoundBlock != null)
                {
                    FoundBlock.Text += "[" + DateTime.Now + "]   " + InReport + "\r\n";
                }

                Instance.BuildOutlogLogScrollViewer.ScrollToVerticalOffset(Instance.BuildOutlogLogScrollViewer.ExtentHeight + 20);
            }, null);
        }

        public static string GetProjectName()
        {
            return MainWindow.ProjectWorker.GetProjectName();
        }

        public void BuildModulePanel()
        {
            StackPanel ModulePanel = (StackPanel)Instance.FindName("ModuleviewContainer");
            if (ModulePanel != null)
            {
                foreach(ModuleData Module in MainWindow.ProjectWorker.Proxy.Modules)
                {
                    ModuleView NewModuleView = new ModuleView(Module);
                    ModulePanel.Children.Add(NewModuleView);
                }
            }
        }

        private void NewModuleButton_Click(object sender, RoutedEventArgs e)
        {
            NewModuleView NewModuleWindow = new NewModuleView();
            NewModuleWindow.Show();
        }

        private void SourceControlButton_Click(object sender, RoutedEventArgs e)
        {
            string Url = "ec2-18-217-78-116.us-east-2.compute.amazonaws.com:1666";
            string User = "toms";
            string Workspace = "Tom";

            Task.Factory.StartNew(() =>
            {
                SourceControlHandler.Connect(Url, User, Workspace);
            });           
        }
    }
}
