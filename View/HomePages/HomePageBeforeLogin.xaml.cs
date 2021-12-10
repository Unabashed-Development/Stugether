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

        /// <summary>
        /// Initializes the mediator for the authentication viewmodel.
        /// </summary>
        private MainAuthenticationViewModel InitializeMediator()
        {
            NavigationStore authenticationNavigationStore = new NavigationStore(); // Create navigation store for the ViewModel
            authenticationNavigationStore.CurrentViewModel = new LoginViewModel(authenticationNavigationStore); // Set the LoginViewModel as the current view model
            return new MainAuthenticationViewModel(authenticationNavigationStore); // Create a new MainAuthenticationViewModel
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainAuthenticationViewModel currentAuthenticationViewModel = InitializeMediator();

            AuthenticationWindow authenticationWindow = new AuthenticationWindow()
            { // Sets the data context for the authentiction window to MainAuthenticationViewModel.
                DataContext = currentAuthenticationViewModel
            };

            authenticationWindow.Show(); // Show the authentication window

            // Close the window once the authentication has finished
            currentAuthenticationViewModel.FinishLoggingIn += () => authenticationWindow.Close();
        }
    }
}
