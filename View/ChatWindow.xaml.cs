using System;
using System.Windows;
using System.Windows.Controls;
using ViewModel;
using Windows.UI.ViewManagement.Core;

namespace View
{
    /// <summary>
    /// Interaction logic for ChatWindow.xaml
    /// </summary>
    public partial class ChatWindow : Window
    {
        public ChatWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// This method sets the DispatcherDelegate to allow executing code on UI thread, because the ViewModel can't access it.
        /// </summary>
        public void SetDataContextDispatcher()
        {
            ((ChatWindowViewModel)this.DataContext).DispatcherDelegate = (act) =>
            {
                Dispatcher.Invoke(act);
            };
            
        }

        private bool AutoScroll = true;
        private void ChatScrollViewer_ScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            if (e.ExtentHeightChange == 0)
            {
                AutoScroll = ChatScrollViewer.VerticalOffset == ChatScrollViewer.ScrollableHeight;
            }

            if (AutoScroll && e.ExtentHeightChange != 0)
            {
                ChatScrollViewer.ScrollToVerticalOffset(ChatScrollViewer.ExtentHeight);
            }
        }

        /// <summary>
        /// Opens the Windows emoji panel.
        /// </summary>
        private void Emoji_Click(object sender, RoutedEventArgs e)
        {
            CoreInputView.GetForCurrentView().TryShow(CoreInputViewKind.Emoji);
            ChatMessageInput.Focus();
        }

        private void OnLostFocus(object sender, EventArgs e)
        {
            ((ChatWindowViewModel)DataContext).ChatWindowHasFocus = false;
            System.Diagnostics.Debug.WriteLine("Lost focus");
        }

        private void OnGotFocus(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine("Got focus");
            // Do this, so that when receiving a new message and the window is focused, it will be marked as seen immediately
            ((ChatWindowViewModel)DataContext).ChatWindowHasFocus = true;


            ((ChatWindowViewModel)DataContext).SeenChatMessages();
        }
    }
}
