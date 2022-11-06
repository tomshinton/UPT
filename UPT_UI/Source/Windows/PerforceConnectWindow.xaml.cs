using System.Windows;

namespace UPT_UI.Windows
{
    public partial class PerforceConnectWindow : Window
    {
        public PerforceConnectWindow()
        {
            InitializeComponent();
        }

        private void CancelButton_OnClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void ConnectButton_OnClick(object sender, RoutedEventArgs e)
        { 
            MainWindow.ReportProgress("Attempting to initialise P4 connection");
            MainWindow.SourceControlHandler.Connect(ServerURLBox.Text, UserNameBox.Text, WorkspaceNameBox.Text, PasswordBox.Text);
            this.Close();
        }
    }
}