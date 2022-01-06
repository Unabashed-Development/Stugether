using Model;
using System.Windows;
using System.Windows.Controls;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for OverviewMatches.xaml
    /// </summary>
    public partial class OverviewMatches : Page
    {
        public OverviewMatches()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Initialize a new window with the profile from the RoutedEventArgs and the constructor of ProfileWindow.
        /// </summary>
        private void Profile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.ProfileWindowFrame.Content = new ProfilePage(new ProfilePageViewModel(((Button)sender).DataContext as Profile));

            profileWindow.Show(); // Show the authentication window
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            ChatWindow chatWindow = new ChatWindow();
            chatWindow.DataContext = new ChatWindowViewModel(((Button)sender).DataContext as Profile);
            chatWindow.SetDataContextDispatcher();
            chatWindow.Show();
        }
    }
}
