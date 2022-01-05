using Gateway;
using Microsoft.Toolkit.Uwp.Notifications;
using Model;
using System.Threading;
using ViewModel.Mediators;

namespace ViewModel.Helpers
{
    public static class NotificationHelper
    {
        /// <summary>
        /// Initializes all notification background threads and adds them to the BackgroundThreads dictionary in Account.
        /// </summary>
        public static void InitializeNotifications()
        {
            if (Account.Authenticated)
            {
                SetAccountNotificationSettings(true);
                Account.BackgroundThreads["MatchNotification"] = new Timer(new TimerCallback(CheckForNewMatches), null, 3000, 3000);
            }
            else // If logged out, set the timeouts to infinite (they don't execute) and clear the notification settings
            {
                SetAccountNotificationSettings(false);
                Account.BackgroundThreads["MatchNotification"].Dispose();
            }
        }

        /// <summary>
        /// Sets the notification settings for the logged in account.
        /// </summary>
        /// <param name="set">Determines if the notification settigns need to be set (true) or cleared (false)</param>
        private static void SetAccountNotificationSettings(bool set)
        {
            if (set)
            {
                Account.NotificationSettings = NotificationDataAccess.GetNotificationSettings(Account.UserID.Value);
            }
            else
            {
                Account.NotificationSettings = null;
            }
        }

        /// <summary>
        /// Checks if the user has received any new matches and throws a notification.
        /// </summary>
        private static void CheckForNewMatches(object state)
        {
            // Load the current matches from the database and the matches loaded at the start of the application.
            int currentMatchCount = MatchDataAccess.GetAllMatchesFromUser(Account.UserID.Value, MatchOrLike.Matched).Count;
            int loadedMatchCount = Account.Matches.Count;

            // If the amount of matches are the different, execute
            if (currentMatchCount != loadedMatchCount)
            {
                // Reload the profiles of the matches, so if there are less matches, the profiles get reloaded too
                ViewModelMediators.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value);

                // If the current matches are more (so there is a new match instead of an unmatch), execute
                if (currentMatchCount > loadedMatchCount)
                {
                    ThrowMatchNotification(); // Throw new match notification 
                }
            }
        }

        /// <summary>
        /// Throws a new match notification to Windows using Toast.
        /// Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        /// https://docs.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/send-local-toast?tabs=desktop#step-4-implement-the-activator
        /// </summary>
        private static void ThrowMatchNotification()
        {
            new ToastContentBuilder()
            .AddArgument("OverviewMatches.xaml") // Arguments gets used to open this page if notification is clicked on
            .AddText("Je hebt een nieuwe Stugether match!")
            .AddText("Wat leuk! Kijk snel wie!")
            .Show();
        }
    }
}
