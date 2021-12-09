using System.Windows;
using System.Windows.Controls;
using View.Authentication;
using ViewModel;
using ViewModel.Mediators;
using ViewModel.Stores;

namespace View
{
    /// <summary>
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        public HomePage()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            NavigationStore navigationStore = new NavigationStore(); // Create navigation store for the ViewModel
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore); // Set the LoginViewModel as the current view model
            ViewModelMediators.currentAuthenticationViewModel = new MainAuthenticationViewModel(navigationStore); // Create a new MainAuthenticationViewModel

            AuthenticationWindow authenticationWindow = new AuthenticationWindow()
            { // Sets the data context for the authentiction window to MainAuthenticationViewModel.
                DataContext = ViewModelMediators.currentAuthenticationViewModel
            };

            authenticationWindow.Show(); // Show the authentication window

            // Close the window once the authentication has finished
            ViewModelMediators.currentAuthenticationViewModel.FinishLoggingIn += () => authenticationWindow.Close();
        }
    }
}
