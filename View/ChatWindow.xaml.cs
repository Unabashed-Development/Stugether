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
                if(ChatScrollViewer.VerticalOffset == ChatScrollViewer.ScrollableHeight)
                {
                    AutoScroll = true;
                }
                else
                {
                    AutoScroll = false;
                }
            }

            if (AutoScroll && e.ExtentHeightChange != 0)
            {
                ChatScrollViewer.ScrollToVerticalOffset(ChatScrollViewer.ExtentHeight);
            }
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
