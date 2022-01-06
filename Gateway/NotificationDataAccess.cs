using System;
using System.Data;
using Dapper;
using Model;

namespace Gateway
{
    public static class NotificationDataAccess
    {
        #region Methods
        /// <summary>
        /// Gets the notification settins from a certain user from the database, and if it does not exist yet,
        /// creates them in the database and retrieves them.
        /// </summary>
        /// <param name="userID">The ID of the user the notification settings need to be requested for.</param>
        /// <returns>NotificationSettings class with the boolean values of notification setting preferences.</returns>
        public static NotificationSettings GetNotificationSettings(int userID)
        {
            // Try to retrieve the notification settings from the database
            try
            {
                return RetrieveNotificationSettings(userID);
            }
            // If that did not work, it means it doesn't exist yet for the user, so create the notification settings
            catch (InvalidOperationException)
            {
                InsertNotificationSettings(userID);
                return RetrieveNotificationSettings(userID);
            }
        }

        /// <summary>
        /// Sets new notification settings for a certain user in the database.
        /// </summary>
        /// <param name="notificationSettings">The class with the notification settings.</param>
        /// <param name="userID">The ID of the user the notification settings need to be updated for.</param>
        public static void SetNotificationSettings(NotificationSettings notificationSettings, int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute(
                    $"UPDATE NotificationSettings " +
                    $"SET Matches = @Matches, Likes = @Likes, Chat = @Chat " +
                    $"WHERE UserID = {userID}", notificationSettings);
            }
        }

        /// <summary>
        /// Accesses the database and inserts new notification settings for a certain user.
        /// </summary>
        /// <param name="userID">The ID of the user the notification settings need to be inserted for.</param>
        private static void InsertNotificationSettings(int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                connection.Execute($"INSERT INTO NotificationSettings(UserID) VALUES ({userID})");
            }
        }

        /// <summary>
        /// Accesses the database and requests the notification settings for a certain user.
        /// </summary>
        /// <param name="userID">The ID of the user the notification settings need to be requested for.</param>
        /// <returns>NotificationSettings class with the boolean values of notification setting preferences.</returns>
        private static NotificationSettings RetrieveNotificationSettings(int userID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB")))
            {
                return connection.QuerySingle<NotificationSettings>($"SELECT Matches, Likes, Chat FROM NotificationSettings WHERE UserID = {userID}");
            }
        }
        #endregion
    }
}
