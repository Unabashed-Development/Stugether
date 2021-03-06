using Model;
using System.Linq;
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
            if (((Button)sender).DataContext is Profile profile && !FocusOpenedWindow<ProfileWindow>(profile))
            {
                ProfileWindow profileWindow = new ProfileWindow();
                profileWindow.ProfileWindowFrame.Content = new ProfilePage(new ProfilePageViewModel(profile));

                profileWindow.Show();
            }
        }

        /// <summary>
        /// Initialize a new window with the matchprofile from the RoutedEventArgs and the constructor of ProfileWindow.
        /// </summary>
        private void MatchingProfile_Click(object sender, RoutedEventArgs e)
        {
            ProfileWindow profileWindow = new ProfileWindow();
            profileWindow.ProfileWindowFrame.Content = new MatchingProfilePage(new MatchingProfilePageViewModel(((Button)sender).DataContext as Profile));

            profileWindow.Show();
        }

        private void Chat_Click(object sender, RoutedEventArgs e)
        {
            if (((Button)sender).DataContext is Profile profile && !FocusOpenedWindow<ChatWindow>(profile))
            {
                ChatWindow chatWindow = new ChatWindow
                {
                    DataContext = new ChatWindowViewModel(profile)
                };

                Chat_Base(chatWindow);
            }
        }

        #region Helper methods
        public static void Chat_Base(ChatWindow chatWindow)
        {
            chatWindow.SetDataContextDispatcher();
            chatWindow.Show();
        }

        /// <summary>
        /// Tries to focus and open an already opened window for a certain user.
        /// </summary>
        /// <typeparam name="T">Type of Window, which can either be ChatWindow or ProfileWindow.</typeparam>
        /// <param name="userID">The user ID the window is possibly opened of.</param>
        /// <returns>True if it has succeeded and false if not.</returns>
        public static bool FocusOpenedWindow<T>(int userID) where T : Window
        {
            bool success = false;
            Application.Current.Dispatcher.Invoke(delegate // Dispatcher delegate for threads
            {
                // Check if a window is already open for the user, and if so, focus it instead of opening a new window
                foreach (T t in Application.Current.Windows.OfType<T>())
                {
                    if (typeof(T) == typeof(ChatWindow))
                    {
                        if ((t.DataContext as ChatWindowViewModel).Receiver.UserID == userID)
                        {
                            success = true;
                        }
                    }
                    else if (typeof(T) == typeof(ProfileWindow))
                    {
                        if ((((t as ProfileWindow).ProfileWindowFrame.Content as ProfilePage).DataContext as ProfilePageViewModel).UserID == userID)
                        {
                            success = true;
                        }
                    }
                    if (success)
                    {
                        t.WindowState = WindowState.Normal;
                        t.Focus();
                        break;
                    }
                }
            });
            return success;
        }

        /// <summary>
        /// Overload for FocusOpenedWindow that takes in a profile instead of an user ID.
        /// </summary>
        /// <typeparam name="T">Type of Window, which can either be ChatWindow or ProfileWindow.</typeparam>
        /// <param name="profile">The profile the window is possibly opened of.</param>
        /// <returns>True if it has succeeded and false if not.</returns>
        private static bool FocusOpenedWindow<T>(Profile profile) where T : Window => FocusOpenedWindow<T>(profile.UserID);
        #endregion
    }
}