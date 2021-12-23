using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModel.Commands;

namespace ViewModel
{
    /// <summary>
    /// The viewmodel for viewing chats
    /// </summary>
    public class ChatWindowViewModel : ObservableObject
    {
        private ObservableCollection<ChatMessage> chatMessages = new ObservableCollection<ChatMessage>();
        private Profile receiver;

        /// <summary>
        /// The other end of this chat
        /// </summary>
        public Profile Receiver
        {
            get => receiver;
            set
            {
                receiver = value;
                RaisePropertyChanged("Receiver");
                RaisePropertyChanged("Title");
            }
        }
        /// <summary>
        /// The viewing end of this chat
        /// </summary>
        /// <see cref="Profile.LoggedInProfile"/>
        public Profile Sender => Profile.LoggedInProfile;

        /// <summary>
        /// The list of messages send between the viewing side and the other side of this chat
        /// </summary>
        public ObservableCollection<ChatMessage> ChatMessages
        {
            get => chatMessages;
            set
            {
                chatMessages = value;
                RaisePropertyChanged("ChatMessages");
            }
        }
        /// <summary>
        /// The title of the Window, showing the full name of the receiving side and amount of unread messages
        /// </summary>
        public string Title
        {
            get
            {
                return Receiver.FirstName + " " + Receiver.LastName;
            }
        }

        /// <summary>
        /// Creates an empty conversation
        /// </summary>
        public ChatWindowViewModel()
        {

        }
        /// <summary>
        /// Creates a conversationViewModel with given userId
        /// </summary>
        /// <param name="otherUserId">The UserId to communicate with</param>
        public ChatWindowViewModel(int otherUserId) : this(ProfileDataAccess.LoadProfile(otherUserId)) { }

        /// <summary>
        /// Creates a conversationViewModel with given profile
        /// </summary>
        /// <param name="otherUser">The profile to communicate with</param>
        public ChatWindowViewModel(Profile otherUser)
        {
            Receiver = otherUser;
            ChatMessages = new ObservableCollection<ChatMessage>(ChatDataAccess.LoadChatMessages(Sender.UserID, Receiver.UserID));
        }
    }
}
