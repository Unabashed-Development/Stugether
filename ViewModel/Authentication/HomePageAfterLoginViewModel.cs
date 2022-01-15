using Model;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Mediators;

namespace ViewModel.HomePages
{
    public class HomePageAfterLoginViewModel : ObservableObject
    {
        public string Email => Account.Email;

        /// <summary>
        /// Logs the user out by clearing the Account properties.
        /// </summary>
        private void Logout()
        {
            Account.Email = null;
            Account.UserID = null;
            Account.NotifiedChatMessages = null;
            ViewModelMediators.Authenticated = false;
        }

        /// <summary>
        /// Logs the user out by clearing Account data.
        /// </summary>
        public ICommand LogoutCommand => new RelayCommand(Logout, () => true);
    }
}
