using System.Windows;

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
            AuthenticationWindow authenticationWindow = new AuthenticationWindow();
            authenticationWindow.Show();
        }
    }
}
