using Gateway;
using Microsoft.Toolkit.Uwp.Notifications;
using Model;
using System.Threading;
using ViewModel.Mediators;

namespace ViewModel.Helpers
{
    public static class NotificationHelper
    {
        #region Methods
        /// <summary>
        /// Initializes all notification background threads and adds them to the BackgroundThreads dictionary in Account.
        /// </summary>
        public static void InitializeAllNotifications()
        {
            if (Account.Authenticated)
            {
                SetAccountNotificationSettings(true);
                SetAllNotificationTimers(true, false);
            }
            else // If logged out, set the timeouts to infinite (they don't execute) and clear the notification settings
            {
                SetAccountNotificationSettings(false);
                SetAllNotificationTimers(false, false);
            }
        }

        /// <summary>
        /// Sets the notification settings for the logged in account.
        /// </summary>
        /// <param name="set">Determines if the notification timers need to be set (true) or disposed of (false)</param>
        /// <param name="reRun">Determines if the method is rerun while the user has already been logged in.
        /// Removes background threads if necessary. Generally used for when the user changed his notification preferences.</param>
        public static void SetAllNotificationTimers(bool set, bool reRun)
        {
            string[] keyArray = new string[]
                {
                    "MatchNotification",
                    "LikeNotification",
                    "ChatNotification"
                };
            if (!set || reRun)
            {
                foreach (string s in keyArray)
                {
                    DisposeOfTimerFromBackgroundThreads(s);
                }
            }
            if (set)
            {
                if (Account.NotificationSettings.Matches)
                {
                    Account.BackgroundThreads[keyArray[0]] = new Timer(new TimerCallback(MatchNotification), null, 3000, 3000);
                }
                if (Account.NotificationSettings.Likes)
                {
                    // Implement
                }
                if (Account.NotificationSettings.Chat)
                {
                    // Implement
                }
            }

            // Unused dynamic code
            //PropertyInfo[] properties = typeof(NotificationSettings).GetProperties();
            //foreach (PropertyInfo p in properties)
            //{
            //    if ((bool)p.GetValue(Account.NotificationSettings, null))
            //    {
            //        Account.BackgroundThreads[$"{p.Name}Notifications"] = new Timer(new TimerCallback(), null, 3000, 3000);
            //    }
            //}
        }

        /// <summary>
        /// Tries to dispose and remove a key/value pair of timer from BackgroundThreads.
        /// </summary>
        /// <param name="key">The key the timer needs to be stopped and removed for.</param>
        private static void DisposeOfTimerFromBackgroundThreads(string key)
        {
            if (Account.BackgroundThreads.ContainsKey(key))
            {
                Account.BackgroundThreads[key].Dispose();
                Account.BackgroundThreads.Remove(key);
            }
        }

        /// <summary>
        /// Sets the notification settings for the logged in account.
        /// </summary>
        /// <param name="set">Determines if the notification settigns need to be set (true) or cleared (false)</param>
        private static void SetAccountNotificationSettings(bool set)
        {
            Account.NotificationSettings = set ? NotificationDataAccess.GetNotificationSettings(Account.UserID.Value) : null;
        }
        #endregion

        #region Notification methods
        /// <summary>
        /// Checks if the user has received any new matches and throws a notification.
        /// </summary>
        private static void MatchNotification(object state)
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
        #endregion
    }
}
