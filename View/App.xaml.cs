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
