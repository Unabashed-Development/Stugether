using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Windows.Input;
using ViewModel.Commands;

namespace ViewModel
{
    /// <summary>
    /// The viewmodel for viewing chats
    /// </summary>
    public class ChatWindowViewModel : ObservableObject
    {
        #region Common Properties
        private ObservableCollection<ChatMessage> chatMessages = new ObservableCollection<ChatMessage>();
        private readonly object chatMessagesLock = new object();
        private Profile receiver;
        private readonly string backgroundThreadName = "";

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
            set
            {
                receiver = value;
                RaisePropertyChanged(nameof(Receiver));
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
                    RaisePropertyChanged(nameof(ChatMessages));
                }
            }
        }
        /// <summary>
        /// The title of the Window, showing the full name of the receiving side and amount of unread messages
        /// </summary>
        public string Title => Receiver.FirstName + " " + Receiver.LastName;
        #endregion

        #region Loading and reading messages
        /// <summary>
        /// Fetches and filters the new chat messages.
        /// </summary>
        private void RefreshChats() { RefreshChats(null); }
        /// <summary>
        /// Fetches and filters the new chat messages.
        /// </summary>
        /// <param name="state">set this to true to run OnNewMessagesLoaded synchronous. It does'nt work when loaded with UI bindings</param>
        public void RefreshChats(object state)
        {
            SeenChatMessages();
            List<ChatMessage> updatedChats = ChatDataAccess.LoadChatMessages(Sender.UserID, Receiver.UserID);
            IEnumerable<ChatMessage> newChats = from msg in updatedChats
                                                where !ChatMessages.Contains(msg)
                                                select msg;
            System.Diagnostics.Debug.WriteLine("New messages: " + newChats.Count(), backgroundThreadName);

            if (state != null && state.GetType() == typeof(bool) && (bool)state == true)
            {
                OnNewMessagesLoaded(newChats);
            }
            else
            {
                DispatcherDelegate?.Invoke(() => { OnNewMessagesLoaded(newChats); });
            }
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
                    // Message is updated
                    if (chatMessages.Any((anyMsg) => { return anyMsg.MessageId == msg.MessageId && anyMsg.FromUserId == msg.FromUserId && anyMsg.ToUserId == anyMsg.ToUserId; }))
                    {
                        ChatMessage messageToReplace = chatMessages.First((firstMsg) => { return firstMsg.MessageId == msg.MessageId && firstMsg.FromUserId == msg.FromUserId && firstMsg.ToUserId == firstMsg.ToUserId; });
                        chatMessages[chatMessages.IndexOf(messageToReplace)] = msg;
                    }
                    // Message is new
                    else
                    {
                        chatMessages.Add(msg);
                    }
                }
            }
        }
        #endregion

        #region Sending messages
        private string sendMessageContent = "";

        /// <summary>
        /// The text presented in the textbox for sending a new message
        /// </summary>
        public string SendMessageContent
        {
            get { return sendMessageContent; }
            set
            {
                sendMessageContent = value;
                RaisePropertyChanged("SendMessageContent");
            }
        }


        /// <summary>
        /// When initiated, it pushes a new message to the database, so the message is sent
        /// </summary>
        public ICommand SendChatMessageCommand => new RelayCommand(
            () =>
        {
            if (!string.IsNullOrEmpty(SendMessageContent) && !string.IsNullOrWhiteSpace(SendMessageContent))
            {
                ChatDataAccess.SendMessage(Sender.UserID, Receiver.UserID, SendMessageContent);
                SendMessageContent = "";
            }
        },
            () => true);
        #endregion

        #region Seen status
        /// <summary>
        /// If the window is focused, select all messages received and visible, and mark them as seen
        /// </summary>
        public void SeenChatMessages()
        {
            if (ChatWindowHasFocus)
            {
                foreach (ChatMessage msg in from msg in ChatMessages where msg.Seen == false && msg.ToUserId == Sender.UserID select msg)
                {
                    ChatDataAccess.SetMessageSeen(msg.ToUserId, msg.FromUserId, msg.MessageId);
                }
            }
        }

        public bool ChatWindowHasFocus { internal get; set; }
        #endregion

        #region Construction and destruction
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
            //ChatMessages = new ObservableCollection<ChatMessage>(ChatDataAccess.LoadChatMessages(Sender.UserID, Receiver.UserID));
            RefreshChats(true);
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
        #endregion
    }
}
