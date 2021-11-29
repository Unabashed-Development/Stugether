using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
        /// Occurs when a navigation item is clicked to navigate to its page
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The event data.</param>
        private void MainWindowNavigationItem_Click(object sender, RoutedEventArgs e)
        {
            MainWindowNavigationItem mwniSender = (MainWindowNavigationItem)sender;
            tbContentTitle.Text = (string)mwniSender.Content;
            frContent.Navigate(mwniSender.Page);
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
