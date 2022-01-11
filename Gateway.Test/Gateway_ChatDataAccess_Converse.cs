using System;
using System.Collections.Generic;
using System.Text;
using Model;
using NUnit.Framework;

namespace Gateway.Test
{
    public class Gateway_ChatDataAccess_Converse
    {
        [Test]
        public void Chat_Converse()
        {
            const int senderId = 1;
            const int receiverId = 12;
            Account.UserID = receiverId;
            ChatMessage chatMessage = new ChatMessage() { Content = "Test_Gateway_ChatDataAccess_Converse", FromUserId = senderId, ToUserId = receiverId };

            void SendMessage()
            {
                ChatDataAccess.SendMessage(chatMessage.FromUserId, chatMessage.ToUserId, chatMessage.Content);
            }

            int CheckMessageCount()
            {
                return ChatDataAccess.GetUnreadMessages(chatMessage.ToUserId, chatMessage.FromUserId);
            }
            
            int CheckMessageCountFromProfile()
            {
                Profile sender = ProfileDataAccess.LoadProfile(senderId);
                sender.UpdateUnreadMessages(receiverId);
                return sender.UnreadChatMessages;
            }

            ChatMessage GetLastUnreadMessage()
            {
                List<ChatMessage> unreadMessages = ChatDataAccess.LoadChatMessages(chatMessage.ToUserId);
                if (unreadMessages.Count == 0) throw new Exception("There should be at least 1 unread message");
                return unreadMessages[unreadMessages.Count-1];
            }

            void SetReadMessage(int messageId)
            {
                ChatDataAccess.SetMessageSeen(chatMessage.ToUserId, chatMessage.FromUserId, messageId);
            }

            List<ChatMessage> GetAllMessages()
            {
                return ChatDataAccess.LoadChatMessages(chatMessage.ToUserId, chatMessage.FromUserId);
            }


            // Determine current unread messages count from sender
            int currentUnreadMessages = CheckMessageCount();
            // Send a message to the receiver
            Assert.DoesNotThrow(SendMessage);
            // Determine new unread message count from sender to check if it is indeed one more
            Assert.IsTrue(CheckMessageCount() == currentUnreadMessages + 1);
            Assert.IsTrue(CheckMessageCountFromProfile() == currentUnreadMessages + 1);

            ChatMessage lastMessage = null;
            // Get the last unread message which should be the one we just sent
            Assert.DoesNotThrow(() => { lastMessage = GetLastUnreadMessage(); });
            Assert.IsTrue(lastMessage.FromUserId == chatMessage.FromUserId &&
                          lastMessage.ToUserId == chatMessage.ToUserId &&
                          lastMessage.Content == chatMessage.Content);
            // Mark this message as read
            Assert.DoesNotThrow(() => { SetReadMessage(lastMessage.MessageId); });
            // The count of unread messages should have been updated now
            Assert.IsTrue(CheckMessageCount() == currentUnreadMessages);
            Assert.IsTrue(CheckMessageCountFromProfile() == currentUnreadMessages);

            // New get the whole conversation and check if the last message is ours
            List<ChatMessage> conversation = new List<ChatMessage>();
            Assert.DoesNotThrow(() => { conversation = GetAllMessages(); });
            lastMessage = conversation[conversation.Count - 1];
            Assert.IsTrue(lastMessage.FromUserId == chatMessage.FromUserId &&
                          lastMessage.ToUserId == chatMessage.ToUserId &&
                          lastMessage.Content == chatMessage.Content);
        }
    }
}
