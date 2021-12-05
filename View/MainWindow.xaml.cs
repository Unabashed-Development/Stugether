using Gateway;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;
using View.Authentication;
using ViewModel;
using ViewModel.Stores;

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
        }

        /// <summary>
        /// Occurs when the back/forward buttons are clicked
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void NavigationNextPreviousButton_Click(object sender, RoutedEventArgs e)
        {
            if ((Button)sender == btPreviousButton)
            {
                frContent.GoBack();
            }
            else if ((Button)sender == btNextButton)
            {
                frContent.GoForward();
            }
        }

        /// <summary>
        /// Occurs when navigation happened in the frame
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void frContent_Navigated(object sender, NavigationEventArgs e)
        {
            btPreviousButton.IsEnabled = ((Frame)sender).CanGoBack;
            btNextButton.IsEnabled = ((Frame)sender).CanGoForward;
            if (((Frame)sender).Content.GetType() == typeof(Page)) 
            {
                tbContentTitle.Text = ((Page)((Frame)sender).Content).Title;
            }
        }

        private void MainWindowNavigationItem_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            AuthenticationNavigationStore navigationStore = new AuthenticationNavigationStore(); // Create navigation store for the ViewModel
            navigationStore.CurrentViewModel = new LoginViewModel(navigationStore); // Set the LoginViewModel as the current view model
            MainAuthenticationViewModel currentViewModel = new MainAuthenticationViewModel(navigationStore); // Create a new MainAuthenticationViewModel

            AuthenticationWindow authenticationWindow = new AuthenticationWindow()
            { // Sets the data context for the authentiction window to MainAuthenticationViewModel.
                DataContext = currentViewModel
            };

            authenticationWindow.Show(); // Show the authentication window

            currentViewModel.RequestClose += () => authenticationWindow.Close();
        }
    }
}
