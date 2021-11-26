using Gateway;
using System.Windows;
using View.Authentication;

namespace View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SSHConnection.InitializeSsh(); // Not MVVM, this needs to be moved somewhere soon
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationWindow authenticationWindow = new AuthenticationWindow();
            authenticationWindow.Show();
        }
    }
}
