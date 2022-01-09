using Gateway;
using Model;
using NUnit.Framework;
using System.Collections.Generic;
using ViewModel.Helpers;
using ViewModel.Mediators;

namespace ViewModel.Test
{
    public class ViewModel_NotificationSettingsViewModel_UpdateNotificationSettingsCommand
    {
        [SetUp]
        public void Setup()
        {
            InitialSetupForTests.ClearFieldsInAccount();
            ViewModelMediators.Matches = new List<Profile>();
            ViewModelMediators.Likes = new List<Profile>();
            Account.UserID = 1;
            Account.Authenticated = true;
            NotificationHelper.InitializeAllNotifications();
        }

        [Test]
        public void UpdateNotificationSettingsCommand_ExistingNotificationSettingsInDatabase_SetsNotificationSettings()
        {
            // Arrange
            Account.NotificationSettings = new NotificationSettings()
            {
                Matches = false,
                Likes = true,
                Chat = false,
                Birthday = true
            };
            NotificationSettingsViewModel viewModel = new NotificationSettingsViewModel();

            // Act
            viewModel.UpdateNotificationSettingsCommand.Execute(null);

            // Prepare assert
            NotificationSettings returnedNotificationSettings = NotificationDataAccess.GetNotificationSettings(Account.UserID.Value);

            // Clean up
            NotificationDataAccess.SetNotificationSettings(new NotificationSettings()
            {
                Matches = true,
                Likes = true,
                Chat = true,
                Birthday = false
            }, Account.UserID.Value);

            // Assert
            Assert.AreEqual(Account.NotificationSettings.Matches, returnedNotificationSettings.Matches, "Matches");
            Assert.AreEqual(Account.NotificationSettings.Likes, returnedNotificationSettings.Likes, "Likes");
            Assert.AreEqual(Account.NotificationSettings.Chat, returnedNotificationSettings.Chat, "Chat");
            Assert.AreEqual(Account.NotificationSettings.Birthday, returnedNotificationSettings.Birthday, "Birthday");
        }
    }
}