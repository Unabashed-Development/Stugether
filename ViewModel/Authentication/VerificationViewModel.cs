using Gateway;
using Model;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Stores;

namespace ViewModel
{
    public class VerificationViewModel : AuthenticationViewModelBase
    {
        #region Methods
        /// <summary>
        /// Verifies if the given value corrosponds with the value in the database. Logs the user in if it corrosponds, but throws an error if it does not.
        /// </summary>
        private void VerificationOnDatabase()
        {
            if (AccountDataAccess.CheckIfVerificationCodeMatches(VerificationCode, Email))
            {
                Account.authenticated = true; // Set the authentication state of the application to true
                Account.userID = AccountDataAccess.GetUserIDFromAccount(Email); // Get the user ID from the account and save it in the application
                OnLoggedIn();
            }
            else
            {
                ErrorMessage = "De verificatie code klopt niet.";
            }
        }
        #endregion

        #region Construction
        public VerificationViewModel(AuthenticationNavigationStore navigationStore)
        {
            base.navigationStore = navigationStore;
        }
        #endregion

        #region Commands
        public ICommand VerifyCommand => new RelayCommand(VerificationOnDatabase, CanExecute);
        #endregion
    }
}
