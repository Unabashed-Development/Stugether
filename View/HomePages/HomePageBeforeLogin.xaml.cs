using System.Windows;
using System.Windows.Controls;
using View.Authentication;
using ViewModel;
using ViewModel.Mediators;
using ViewModel.Stores;

namespace View
{
    /// <summary>
    /// Interaction logic for HomePageBeforeLogin.xaml
    /// </summary>
    public partial class HomePageBeforeLogin : Page
    {
        public HomePageBeforeLogin()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NavigationStore authenticationNavigationStore = new NavigationStore(); // Create navigation store for the ViewModel
            authenticationNavigationStore.CurrentViewModel = new LoginViewModel(authenticationNavigationStore); // Set the LoginViewModel as the current view model
            MainViewModel currentAuthenticationViewModel = new MainViewModel(authenticationNavigationStore); // Create a new MainAuthenticationViewModel

            AuthenticationWindow authenticationWindow = new AuthenticationWindow()
            { // Sets the data context for the authentiction window to MainAuthenticationViewModel.
                DataContext = currentAuthenticationViewModel
            };

            authenticationWindow.Show(); // Show the authentication window

            // Close the window once the authentication has finished
            ViewModelMediators.AuthenticationStateChanged += () => authenticationWindow.Close();
        }
    }
}
