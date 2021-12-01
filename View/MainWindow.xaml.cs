using Gateway;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using View.Authentication;

namespace View
{
    /// <summary>
    /// The main window of the project
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SSHConnection.InitializeSsh(); // TODO: Not MVVM, this needs to be moved somewhere soon
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow authenticationWindow = new AuthenticationWindow();
            authenticationWindow.Show();
        }
    }
}
