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
    }
}
