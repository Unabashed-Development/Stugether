using Dapper;
using Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

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
        /// Gets the amount of unread messages between this profile and given user id
        /// </summary>
        /// <param name="profile">The profile on the other end</param>
        /// <param name="ownUserId">The person viewing the chat</param>
        /// <returns>The amount of unread messages in this chat</returns>
        public static int GetUnreadMessages(this Profile profile, int ownUserId)
        {
            return GetUnreadMessages(profile.UserID, ownUserId);
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
            return connection.QuerySingle<int>("SELECT COUNT(Seen) FROM ChatMessage WHERE FromUserId=@other AND ToUserId=@own AND Seen=1", new { own = ownUserId, other = otherUserId });
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
