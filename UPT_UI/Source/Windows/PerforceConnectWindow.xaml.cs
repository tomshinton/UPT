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
            MainWindow.SourceControlHandler.Connect(ServerURLBox.Text, UserNameBox.Text, WorkspaceNameBox.Text, PasswordBox.Password);
            this.Close();
        }

        private void ServerURLBoxWatermark_OnGotFocus(object sender, RoutedEventArgs e)
        {
            ServerURLBoxWatermark.Visibility = Visibility.Collapsed;
            ServerURLBox.Visibility = Visibility.Visible;
            ServerURLBox.Focus();
        }

        private void ServerURLBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(ServerURLBox.Text))
            {
                ServerURLBox.Visibility = Visibility.Collapsed;
                ServerURLBoxWatermark.Visibility = Visibility.Visible;
            }
        }

        private void UserNameBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(UserNameBox.Text))
            {
                UserNameBox.Visibility = Visibility.Collapsed;
                UserNameBoxWatermark.Visibility = Visibility.Visible;
            }
        }

        private void UserNameBoxWatermark_OnGotFocus(object sender, RoutedEventArgs e)
        {
            UserNameBoxWatermark.Visibility = Visibility.Collapsed;
            UserNameBox.Visibility = Visibility.Visible;
            UserNameBox.Focus();
        }

        private void WorkspaceNameBox_OnLostFocus(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(WorkspaceNameBox.Text))
            {
                WorkspaceNameBox.Visibility = Visibility.Collapsed;
                WorkspaceNameBoxWatermark.Visibility = Visibility.Visible;
            }
        }

        private void WorkspaceNameBoxWatermark_OnGotFocus(object sender, RoutedEventArgs e)
        {
            WorkspaceNameBoxWatermark.Visibility = Visibility.Collapsed;
            WorkspaceNameBox.Visibility = Visibility.Visible;
            WorkspaceNameBox.Focus();
        }
    }
}