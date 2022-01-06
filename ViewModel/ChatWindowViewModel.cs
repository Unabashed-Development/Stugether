using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using ViewModel.Commands;

namespace ViewModel
{
    /// <summary>
    /// The viewmodel for viewing chats
    /// </summary>
    public class ChatWindowViewModel : ObservableObject
    {
        private ObservableCollection<ChatMessage> chatMessages = new ObservableCollection<ChatMessage>();
        private readonly object chatMessagesLock = new object();
        private Profile receiver;
        private string backgroundThreadName = "";

        /// <summary>
        /// The delegate which allows executing on UI thread.
        /// This is assigned in the View containing a Dispatcher.Invoke implementation.
        /// </summary>
        public Action<Action> DispatcherDelegate { internal get; set; }

        /// <summary>
        /// The other end of this chat
        /// </summary>
        public Profile Receiver
        {
            get => receiver;
            internal set
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
            get
            {
                lock (chatMessagesLock)
                {
                    return chatMessages;
                }
            }
            set
            {
                lock (chatMessagesLock)
                {
                    chatMessages = value;
                    RaisePropertyChanged("ChatMessages");
                }
            }
        }
        /// <summary>
        /// The title of the Window, showing the full name of the receiving side and amount of unread messages
        /// </summary>
        public string Title => Receiver.FirstName + " " + Receiver.LastName;

        /// <summary>
        /// Fetches and filters the new chat messages.
        /// </summary>
        private void RefreshChats() { RefreshChats(null); }
        /// <summary>
        /// Fetches and filters the new chat messages.
        /// </summary>
        /// <param name="state">Unused parameter to comply with TimerCallback</param>
        private void RefreshChats(object? state)
        {
            List<ChatMessage> updatedChats = ChatDataAccess.LoadChatMessages(Sender.UserID, Receiver.UserID);
            IEnumerable<ChatMessage> newChats = from msg in updatedChats
                                                where !ChatMessages.Contains(msg)
                                                select msg;
            System.Diagnostics.Debug.WriteLine("New messages: " + newChats.Count(), backgroundThreadName);

            DispatcherDelegate?.Invoke(() => { OnNewMessagesLoaded(newChats); });
        }

        /// <summary>
        /// This method updates the ObservableObject with chat messages.
        /// </summary>
        /// <remarks><strong>Warning:</strong> This has to be executed on the same thread as the variable chatMessages</remarks>
        /// <param name="messages">A list containing the new messages to be added to ChatMessages.</param>
        private void OnNewMessagesLoaded(IEnumerable<ChatMessage> messages)
        {
            foreach (ChatMessage msg in messages)
            {
                lock (chatMessagesLock)
                {
                    chatMessages.Add(msg);
                }
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
            backgroundThreadName = "Chat " + Receiver.FirstName + Receiver.LastName;
            Account.BackgroundThreads[backgroundThreadName] = new Timer(RefreshChats, null, 500, 500);
        }

        /// <summary>
        /// Destructs ChatWindowViewModel, and so stops the background timer for updating chats
        /// </summary>
        ~ChatWindowViewModel()
        {
            Account.BackgroundThreads[backgroundThreadName].Dispose();
            Account.BackgroundThreads.Remove(backgroundThreadName);
        }
    }
}
