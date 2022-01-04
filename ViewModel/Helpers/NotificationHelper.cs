using Microsoft.Toolkit.Uwp.Notifications;
using Model;
using System.Threading;
using ViewModel.Mediators;

namespace ViewModel.Helpers
{
    public static class NotificationHelper
    {
        public static void InitializeNotificationThreads()
        {
            if (Account.Authenticated)
            {
                Account.BackgroundThreads["MatchNotification"] = new Timer(new TimerCallback(ThrowNotification), null, 3000, 3000);
            }
            else
            {
                Account.BackgroundThreads["MatchNotification"].Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        // https://docs.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/send-local-toast?tabs=desktop#step-4-implement-the-activator
        public static void ThrowNotification(object state)
        {
            ViewModelMediators.AuthenticationStateChanged += InitializeNotificationThreads;
            new ToastContentBuilder()
            .AddArgument("action", "viewConversation")
            .AddArgument("conversationId", 9813)
            .AddText("Andrew sent you a picture")
            .AddText("Check this out, The Enchantments in Washington!")
            .Show();
        }
    }
}
