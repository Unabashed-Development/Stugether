using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
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
    }
}
