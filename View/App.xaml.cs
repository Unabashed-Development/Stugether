using Microsoft.Toolkit.Uwp.Notifications;
using System.Windows;

namespace View
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            // Listen to notification activation
            ToastNotificationManagerCompat.OnActivated += toastArgs =>
            {
                // Obtain the arguments from the notification
                ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

                // Need to dispatch to UI thread if performing UI operations
                Current.Dispatcher.Invoke(delegate
                {
                    // Open the main window when a notification is clicked
                    MainWindow.Activate();
                });
            };

            base.OnStartup(e);
        }
    }
}
