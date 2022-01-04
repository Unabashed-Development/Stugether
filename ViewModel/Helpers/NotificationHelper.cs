using Gateway;
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
                Account.BackgroundThreads["MatchNotification"] = new Timer(new TimerCallback(CheckForNewMatches), null, 3000, 3000);
            }
            else
            {
                Account.BackgroundThreads["MatchNotification"].Change(Timeout.Infinite, Timeout.Infinite);
            }
        }

        public static void CheckForNewMatches(object state)
        {
            int currentMatchCount = MatchDataAccess.GetAllMatchesFromUser(Account.UserID.Value, MatchOrLike.Matched).Count;
            int loadedMatchCount = Account.Matches.Count;
            if (currentMatchCount != loadedMatchCount)
            {
                ViewModelMediators.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value); // Reload the profiles of the matches 
                if (currentMatchCount > loadedMatchCount)
                {
                    ThrowMatchNotification(); // Throw new match notification 
                }
            }
        }

        // Requires Microsoft.Toolkit.Uwp.Notifications NuGet package version 7.0 or greater
        // https://docs.microsoft.com/en-us/windows/apps/design/shell/tiles-and-notifications/send-local-toast?tabs=desktop#step-4-implement-the-activator
        public static void ThrowMatchNotification()
        {
            ViewModelMediators.AuthenticationStateChanged += InitializeNotificationThreads;
            new ToastContentBuilder()
            .AddText("Je hebt een nieuwe Stugether match!")
            .AddText("Wat leuk! Kijk snel wie!")
            .Show();
        }
    }
}
