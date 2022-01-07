using Gateway;
using Microsoft.Toolkit.Uwp.Notifications;
using Model;
using System.Threading;
using ViewModel.Mediators;
using System.Collections.Generic;

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
                    Account.BackgroundThreads[keyArray[0]] = new Timer(MatchOrLikeNotification, MatchOrLike.Matched, 5000, 5000);
                }
                if (Account.NotificationSettings.Likes)
                {
                    Account.BackgroundThreads[keyArray[1]] = new Timer(MatchOrLikeNotification, MatchOrLike.Liked, 5000, 5000);
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
        private static void MatchOrLikeNotification(object matchOrLike)
        {
            HashSet<int> current;
            HashSet<int> previous = new HashSet<int>();
            List<Profile> profileList;

            // Prepares data depending on the match or like handling
            if ((MatchOrLike)matchOrLike == MatchOrLike.Matched)
            {
                current = new HashSet<int>(MatchDataAccess.GetAllMatchesFromUser(Account.UserID.Value, MatchOrLike.Matched));
                profileList = ViewModelMediators.Matches;
            }
            else
            {
                current = new HashSet<int>(MatchDataAccess.GetReceivedLikesFromUser(Account.UserID.Value));
                profileList = ViewModelMediators.Likes;
            }

            // Add the user ID's to the HashSet
            foreach (Profile p in profileList)
            {
                previous.Add(p.UserID);
            }

            // Returns only new user ID's
            current.ExceptWith(previous);

            // If the amounts are different OR there is at least 1 new user...
            if (current.Count != previous.Count || current.Count > 0)
            {
                // ...reload the profiles
                if ((MatchOrLike)matchOrLike == MatchOrLike.Matched)
                {
                    ViewModelMediators.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value);
                }
                else
                {
                    ViewModelMediators.Likes = MatchHelper.LoadProfilesOfLikes(Account.UserID.Value);
                }

                // If the current amount are more (so there is a new user instead of an user who removed you)...
                if (current.Count > 0)
                {
                    // Throw new notification with amount of new matches or likes
                    ThrowMatchOrLikeNotification((MatchOrLike)matchOrLike, current.Count);
                }
            }
        }

        /// <summary>
        /// Throws a new match or like notification to Windows using Toast.
        /// Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        /// https://docs.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/send-local-toast?tabs=desktop#step-4-implement-the-activator
        /// </summary>
        /// <param name="matchOrLike">Determines if a match or a like notification needs to be shown.</param>
        private static void ThrowMatchOrLikeNotification(MatchOrLike matchOrLike, int amountOfMatchesOrLikes)
        {
            string topText;
            string bottomText;

            if (matchOrLike == MatchOrLike.Matched)
            {   
                topText = $"Je hebt {amountOfMatchesOrLikes} nieuwe Stugether match!";
                bottomText = "Wat leuk! Kijk snel wie!";
                if (amountOfMatchesOrLikes > 1)
                {
                    topText = topText.Remove(topText.Length - 1);
                    topText += "es!";
                }
            }
            else
            {
                topText = $"Je hebt {amountOfMatchesOrLikes} nieuwe Stugether like!";
                bottomText = "Interessant... wie zou dat zijn?";
                if (amountOfMatchesOrLikes > 1)
                {
                    topText = topText.Remove(topText.Length - 1);
                    topText += "s!";
                }
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
