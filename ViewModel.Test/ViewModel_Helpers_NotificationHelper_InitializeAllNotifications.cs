using Model;
using NUnit.Framework;
using ViewModel.Helpers;
using ViewModel.Mediators;
using System.Collections.Generic;

namespace ViewModel.Test
{
    public class ViewModel_Helpers_NotificationHelper_InitializeAllNotifications
    {
        [SetUp]
        public void Setup()
        {
            InitialSetupForTests.ClearFieldsInAccount();
            Account.UserID = 1; // Test user
            ViewModelMediators.Matches = new List<Profile>();
            ViewModelMediators.Likes = new List<Profile>();
        }

        [Test]
        public void InitializeAllNotifications_UserLoggedIn_SetsNotificationTimers()
        {
            // Arrange
            Account.Authenticated = true;
            string[] keyArray = new string[]
                {
                    "MatchNotification",
                    "LikeNotification",
                    "ChatNotification"
                };

            // Act
            NotificationHelper.InitializeAllNotifications();

            // Assert
            Assert.IsTrue(Account.NotificationSettings.Matches, "Should be 1 from database");
            Assert.IsTrue(Account.NotificationSettings.Chat, "Should be 1 from database");
            Assert.IsTrue(Account.NotificationSettings.Likes, "Should be 1 from database");
            Assert.IsFalse(Account.NotificationSettings.Birthday, "Should be 1 from database");
            Assert.IsNotNull(Account.BackgroundThreads[keyArray[0]], $"{keyArray[0]} should be set");
            Assert.IsNotNull(Account.BackgroundThreads[keyArray[1]], $"{keyArray[1]} should be set");
            Assert.IsNotNull(Account.BackgroundThreads[keyArray[2]], $"{keyArray[2]} should be set");
        }

        [Test]
        public void InitializeAllNotifications_UserNotLoggedIn_ClearsNotificationTimers()
        {
            // Prepare
            Account.Authenticated = true;
            NotificationHelper.InitializeAllNotifications(); // Simulate a login first

            // Arrange
            Account.Authenticated = false;
            string[] keyArray = new string[]
                {
                    "MatchNotification",
                    "LikeNotification",
                    "ChatNotification"
                };

            // Act
            NotificationHelper.InitializeAllNotifications();

            // Assert
            Assert.IsNull(Account.NotificationSettings, "NotificationSettings shouldn't be set");
            Assert.IsFalse(Account.BackgroundThreads.ContainsKey(keyArray[0]), $"{keyArray[0]} shouldn't be set");
            Assert.IsFalse(Account.BackgroundThreads.ContainsKey(keyArray[1]), $"{keyArray[1]} shouldn't be set");
            Assert.IsFalse(Account.BackgroundThreads.ContainsKey(keyArray[2]), $"{keyArray[2]} shouldn't be set");
        }
    }
}