using Gateway;
using Microsoft.Toolkit.Uwp.Notifications;
using Model;
using System.Threading;
using ViewModel.Mediators;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

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
                    Account.BackgroundThreads[keyArray[0]] = new Timer(MatchOrLikeNotification, MatchOrLike.Matched, 0, 5000);
                }
                if (Account.NotificationSettings.Likes)
                {
                    Account.BackgroundThreads[keyArray[1]] = new Timer(MatchOrLikeNotification, MatchOrLike.Liked, 0, 5000);
                }
                if (Account.NotificationSettings.Chat)
                {
                    Account.BackgroundThreads[keyArray[2]] = new Timer(ChatNotifications, null, 0, 1000);
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
        /// If it is the birthday of some users, check their birthday notification preference and fix the Birthday boolean
        /// </summary>
        /// <param name="profileList">A list of profiles that need their Birthday property checked.</param>
        public static List<Profile> FixBirthdayPreferences(List<Profile> profileList)
        {
            foreach (Profile p in profileList)
            {
                if (p.Birthday)
                {
                    p.Birthday = NotificationDataAccess.GetBirthdayNotificationPreference(p.UserID);
                }
            }
            return profileList;
        }

        private static void ChatNotifications(object state)
        {
            List<ChatMessage> unreadChatMessages = new List<ChatMessage>();

            // If the application has just started and no chat messages have been notified for,
            // retrieve all messages and set them to seen (so there will be no notification)
            if (Account.NotifiedChatMessages == null)
            {
                Account.NotifiedChatMessages = ChatDataAccess.LoadChatMessages(Account.UserID.Value);
                foreach (ChatMessage c in Account.NotifiedChatMessages)
                {
                    c.Seen = true;
                }
            }
            // Retrieve the chat messages again and compare them to the notified chat messages.
            // Set all the newly received chat messages to seen if they have already been seen.
            // Add them to the unseen chat messages list. At the end, save the chat messages to the notified list.
            else
            {
                var newChatMessages = ChatDataAccess.LoadChatMessages(Account.UserID.Value);
                foreach (ChatMessage c in newChatMessages)
                {
                    if (Account.NotifiedChatMessages.Any(p => p.MessageId == c.MessageId))
                    {
                        c.Seen = true;
                    }
                    else
                    {
                        unreadChatMessages.Add(c);
                    }
                }

                foreach (ChatMessage c in unreadChatMessages)
                {
                    Profile chatProfile = Account.Matches.Where(p => p.UserID == c.FromUserId).FirstOrDefault();
                    ThrowChatMessageNotification(chatProfile.FirstName,
                                                 chatProfile.LastName,
                                                 chatProfile.FirstUserMedia,
                                                 c.Content);
                    c.Seen = true;
                }

                Account.NotifiedChatMessages = newChatMessages;
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

        private static void ThrowChatMessageNotification(string firstName, string lastName, Uri firstUserMedia, string Content)
        {
            new ToastContentBuilder()
            .AddArgument("OverviewMatches.xaml") // Arguments gets used to open this page if notification is clicked on
            .AddText($"{firstName} {lastName} stuurde een bericht")
            .AddText(Content)
            .AddAppLogoOverride(firstUserMedia, ToastGenericAppLogoCrop.Circle)
            .Show();
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            Rectangle destRect = new Rectangle(0, 0, width, height);
            Bitmap destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (Graphics graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (ImageAttributes wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }
        #endregion
    }
}