using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using View.Authentication;
using ViewModel;
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
