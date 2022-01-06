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
            else // If logged out, disable timers and clear the notification settings
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
                    DisposeTimersFromBackgroundThreads(s);
                }
            }
            if (set)
            {
                if (Account.NotificationSettings.Matches)
                {
                    Account.BackgroundThreads[keyArray[0]] = new Timer(new TimerCallback(MatchNotification), null, 5000, 5000);
                }
                if (Account.NotificationSettings.Likes)
                {
                    Account.BackgroundThreads[keyArray[1]] = new Timer(new TimerCallback(LikeNotification), null, 5000, 5000);
                }
                if (Account.NotificationSettings.Chat)
                {
                    // Implement
                }
            }
        }

        /// <summary>
        /// Tries to dispose and remove a key/value pair of timer from BackgroundThreads.
        /// </summary>
        /// <param name="key">The key the timer needs to be stopped and removed for.</param>
        private static void DisposeTimersFromBackgroundThreads(string key)
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
                    ThrowMatchOrLikeNotification(MatchOrLike.Matched); // Throw new match notification 
                }
            }
        }

        private static void LikeNotification(object state)
        {
            // Load the current received likes from the database and the likes loaded at the start of the application.
            int currentLikeCount = MatchDataAccess.GetReceivedLikesFromUser(Account.UserID.Value).Count;
            int loadedLikeCount = Account.Likes.Count;

            // If the amount of likes are the different, execute
            if (currentLikeCount != loadedLikeCount)
            {
                // Reload the profiles of the likes, so if there are less likes, the profiles get reloaded too
                ViewModelMediators.Likes = MatchHelper.LoadProfilesOfLikes(Account.UserID.Value);

                // If the current likes are more (so there is a new likes instead of an unlike), execute
                if (currentLikeCount > loadedLikeCount)
                {
                    ThrowMatchOrLikeNotification(MatchOrLike.Liked); // Throw new match notification 
                }
            }
        }

        /// <summary>
        /// Throws a new match or like notification to Windows using Toast.
        /// Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        /// https://docs.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/send-local-toast?tabs=desktop#step-4-implement-the-activator
        /// </summary>
        /// <param name="matchOrLike">Determines if a match or a like notification needs to be shown.</param>
        private static void ThrowMatchOrLikeNotification(MatchOrLike matchOrLike)
        {
            string topText;
            string bottomText;

            if (matchOrLike == MatchOrLike.Matched)
            {
                topText = "Je hebt een nieuwe Stugether match!";
                bottomText = "Wat leuk! Kijk snel wie!";
            }
            else
            {
                topText = "Je hebt een nieuwe Stugether like!";
                bottomText = "Interessant... wie zou dat zijn?";
            }

            new ToastContentBuilder()
            .AddArgument("OverviewMatches.xaml") // Arguments gets used to open this page if notification is clicked on
            .AddText(topText)
            .AddText(bottomText)
            .Show();
        }
        #endregion
    }
}
