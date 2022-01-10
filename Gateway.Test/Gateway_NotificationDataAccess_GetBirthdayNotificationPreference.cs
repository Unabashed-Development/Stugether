using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_NotificationDataAccess_GetBirthdayNotificationPreference
    {
        [Test]
        public void GetBirthdayNotificationPreference_BirthdayPreferenceNegative_ReturnsFalse()
        {
            // Arrange
            const int userID = 1;

            // Act
            bool result = NotificationDataAccess.GetBirthdayNotificationPreference(userID);

            // Assert
            Assert.IsFalse(result);
        }

        [Test]
        public void GetBirthdayNotificationPreference_BirthdayPreferencePositive_ReturnsTrue()
        {
            // Arrange
            const int userID = 12;

            // Act
            bool result = NotificationDataAccess.GetBirthdayNotificationPreference(userID);

            // Assert
            Assert.IsTrue(result);
        }
    }
}