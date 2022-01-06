using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    /// <summary>
    /// A message
    /// </summary>
    public class ChatMessage
    {
        /// <summary>
        /// The identifier used to identify the message
        /// </summary>
        public int MessageId { get; set; }
        /// <summary>
        /// The DateTime when this message was sent
        /// </summary>
        public DateTime SentTime { get; set; }
        /// <summary>
        /// The user who sends this message
        /// </summary>
        public int FromUserId { get; set; }
        /// <summary>
        /// The user who receives this message
        /// </summary>
        public int ToUserId { get; set; }
        /// <summary>
        /// The content of the message
        /// </summary>
        /// <example>
        /// "Hallo!"
        /// </example>
        public string Content { get; set; }
        /// <summary>
        /// Indicates whether the receiving side has seen this message
        /// </summary>
        public bool Seen { get; set; }
        /// <summary>
        /// Indicates the direction this message has been sent, as seen from the user viewing the chat
        /// </summary>
        public MessageDirection Direction { get; set; }

        /// <summary>
        /// The way the message goes
        /// </summary>
        public enum MessageDirection
        {
            Send,
            Received
        }

        /// <summary>
        /// Determines whether the specified chat message is equal to the current chat message.
        /// </summary>
        /// <param name="obj">The object to compare with the current chat message.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() != typeof(ChatMessage))
                return false;
            ChatMessage otherMsg = (ChatMessage)obj;
            if (MessageId != otherMsg.MessageId) return false;
            if (SentTime != otherMsg.SentTime) return false;
            if (FromUserId != otherMsg.FromUserId) return false;
            if (ToUserId != otherMsg.ToUserId) return false;
            if (Content != otherMsg.Content) return false;
            if (Seen != otherMsg.Seen) return false;
            if (Direction != otherMsg.Direction) return false;
            return true;
        }
    }
}
