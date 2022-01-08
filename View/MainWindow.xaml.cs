using System;
using System.Windows;
using System.Windows.Forms;
using System.Drawing;
using System.Reflection;
using System.Windows.Navigation;
using ViewModel.Mediators;
using Microsoft.Toolkit.Uwp.Notifications;
using ViewModel;
using System.Linq;

namespace View
{
    /// <summary>
    /// The main window of the project
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly NotifyIcon ni = new NotifyIcon
        {
            Icon = new Icon(Assembly.GetExecutingAssembly().GetManifestResourceStream("View.stugether_logo.ico")),
            BalloonTipTitle = "Stugether is nu geminimaliseerd",
            BalloonTipText = "Druk op het icoontje om mij weer te openen!",
            Text = "Stugether",
        };

        public MainWindow()
        {
            InitializeComponent();

            ni.DoubleClick +=
                delegate (object sender, EventArgs args)
                {
                    Show();
                    WindowState = WindowState.Normal;
                    ni.Visible = false;
                };

            // Clear navigation history once the authentication has finished
            ViewModelMediators.AuthenticationStateChanged += ClearPagesHistory;

            // Subscribe for the opening of a chat window once a chat notification is clicked
            ToastNotificationManagerCompat.OnActivated += Chat_Notification;
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
                ni.Visible = true;
                ni.ShowBalloonTip(5000);
            }

            base.OnStateChanged(e);
        }

        /// <summary>
        /// Clears the history of the frContent frame located on the main page.
        /// </summary>
        private void ClearPagesHistory()
        {
            if (!frContent.CanGoBack && !frContent.CanGoForward)
            {
                return;
            }

            JournalEntry entry = frContent.RemoveBackEntry();
            while (entry != null)
            {
                entry = frContent.RemoveBackEntry();
            }
        }

        /// <summary>
        /// Opens a new chat window when a chat notification is pressed.
        /// </summary>
        /// <param name="toastArgs">Argument with the user ID of the chat window.</param>
        private void Chat_Notification(ToastNotificationActivatedEventArgsCompat toastArgs)
        {
            string[] toastArgumentArray = toastArgs.Argument.Split('='); // Split the arguments in key and value (array)
            if (toastArgumentArray[0] == "Chat")
            {
                int toastArgumentUserID = int.Parse(toastArgumentArray[1]);
                if (!OverviewMatches.FocusOpenedWindow<ChatWindow>(toastArgumentUserID))
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(delegate // Dispatcher delegate for threads
                    {
                        // If there have been no ChatWindows found for the sending user, open a new window
                        ChatWindow chatWindow = new ChatWindow
                        {
                            DataContext = new ChatWindowViewModel(toastArgumentUserID)
                        };
                        OverviewMatches.Chat_Base(chatWindow);
                    });
                }
            }
        }
    }
}