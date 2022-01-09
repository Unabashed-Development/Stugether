using Dapper;
using Model;
using System.Collections.Generic;
using System.Data;

namespace Gateway
{
    /// <summary>
    /// Communication between database and application for chats
    /// </summary>
    public static class ChatDataAccess
    {
        /// <summary>
        /// Fetch chat messages between two users
        /// </summary>
        /// <param name="OwnUserId">The user viewing the chat</param>
        /// <param name="OtherUserId">The user on the other end of the chat</param>
        /// <returns>A List with ChatMessages between the two given users</returns>
        public static List<ChatMessage> LoadChatMessages(int OwnUserId, int OtherUserId)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            List<ChatMessage> result = new List<ChatMessage>();
            connection.Query<ChatMessage>(
                @"SELECT * FROM ChatMessage
                  WHERE (FromUserId=@own AND ToUserId=@other) OR
                        (FromUserId=@other AND ToUserId=@own)
                  ORDER BY SentTime",
                new { own = OwnUserId, other = OtherUserId }
                ).AsList().ForEach((msg) =>
            {
                msg.Direction = msg.FromUserId == OwnUserId ? ChatMessage.MessageDirection.Send : ChatMessage.MessageDirection.Received;
                result.Add(msg);
            });
            return result;
        }

        /// <summary>
        /// Loads all the chat messages from everyone sent to a certain own user ID.
        /// </summary>
        /// <param name="OwnUserId">The user the messages have been sent to.</param>
        /// <returns>A List with all ChatMessages</returns>
        public static List<ChatMessage> LoadChatMessages(int OwnUserId)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            List<ChatMessage> result = new List<ChatMessage>();
            result = connection.Query<ChatMessage>(
                @"SELECT * FROM ChatMessage
                  WHERE ToUserId = @own
                  AND Seen = 0
                  ORDER BY SentTime",
                new { own = OwnUserId }
                ).AsList();
            return result;
        }

        /// <summary>
        /// Gets the amount of unread messages between this profile and given user id
        /// </summary>
        /// <param name="profile">The profile on the other end</param>
        /// <param name="ownUserId">The person viewing the chat</param>
        public static void UpdateUnreadMessages(this Profile profile, int ownUserId)
        {
            profile.UnreadChatMessages = GetUnreadMessages(ownUserId, profile.UserID);
        }

        /// <summary>
        /// Gets the amount of unread messages between two users
        /// </summary>
        /// <param name="otherUserId">The user on the other end of the chat</param>
        /// <param name="ownUserId">The user viewing the chat</param>
        /// <returns>The amount of unread messages in this chat</returns>
        public static int GetUnreadMessages(int ownUserId, int otherUserId)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            return connection.QuerySingle<int>("SELECT COUNT(Seen) FROM ChatMessage WHERE FromUserId=@other AND ToUserId=@own AND Seen=0", new { own = ownUserId, other = otherUserId });
        }

        /// <summary>
        /// Set the seen status on a specified message
        /// </summary>
        /// <param name="ownUserId">The user viewing the chat</param>
        /// <param name="senderUserId">The user on the other end of the chat</param>
        /// <param name="messageId">The id of the message to be updated</param>
        public static void SetMessageSeen(int ownUserId, int senderUserId, int messageId)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            connection.Query("UPDATE ChatMessage SET Seen = 1 WHERE FromUserId = @from AND ToUserId = @to AND MessageId = @msg", new { from = senderUserId, to = ownUserId, msg = messageId });
        }

        /// <summary>
        /// Creates a new chat message on the database, which can be seen as a sent message
        /// </summary>
        /// <param name="ownUserId">The user who sends the message</param>
        /// <param name="receiverUserId">The user to whom the message is sent</param>
        /// <param name="messageContent">The content of the message</param>
        public static void SendMessage(int ownUserId, int receiverUserId, string messageContent)
        {
            using IDbConnection connection = new System.Data.SqlClient.SqlConnection(FiddleHelper.GetConnectionStringSql("StudentMatcherDB"));
            connection.Query("INSERT INTO ChatMessage (SentTime, FromUserId, ToUserId, Content, Seen) VALUES(DEFAULT, @from, @to, @content, DEFAULT)", new { from = ownUserId, to = receiverUserId, content = messageContent });
        }
    }
}
