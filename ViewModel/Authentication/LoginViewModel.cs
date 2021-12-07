using Gateway;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Helpers;
using Model;
using ViewModel.Stores;

namespace ViewModel
{
    public class LoginViewModel : AuthenticationViewModelBase
    {
        #region Methods
        /// <summary>
        /// Logs the user in and verifies the details with the database. Sets error messages if something is wrong.
        /// </summary>
        private void LoginInDatabase()
        {
            if (Account.email != null && Account.password != null && Account.email.Length > 0 && Account.password.Length > 0)
            {
                if (AccountHelper.IsValidEmail(Email))
                {
                    if (AccountDataAccess.CheckIfAccountExists(Email))
                    {
                        bool verified = AccountHelper.VerifyPassword(Password, AccountDataAccess.GetHashedPassswordFromAccount(Email));
                        if (verified)
                        {
                            if (AccountDataAccess.CheckIfAccountIsVerified(Email))
                            {
                                Account.userID = AccountDataAccess.GetUserIDFromAccount(Email); // Get the user ID from the account and save it in the application
                                CleanUpAccountData();
                                OnLoggedIn();
                            }
                            else
                            {
                                ErrorMessage = "Je account is nog niet geverifieerd.";
                            }
                        }
                        else
                        {
                            ErrorMessage = "Je inloggegevens zijn onjuist.";
                        }
                    }
                    else
                    {
                        ErrorMessage = "Dit account bestaat niet.";
                    }
                }
                else
                {
                    ErrorMessage = "Dit e-mailadres is niet geldig.";
                }
            }
            else
            {
                ErrorMessage = "Niet alle velden zijn ingevuld.";
            }
        }

        /// <summary>
        /// Sets the CurrentViewModel of the navigationStore to the register ViewModel.
        /// </summary>
        private void NavigateToRegister() => navigationStore.CurrentViewModel = new RegisterViewModel(navigationStore);
        #endregion

        #region Construction
        public LoginViewModel(AuthenticationNavigationStore navigationStore) => base.navigationStore = navigationStore;
        #endregion

        #region Commands
        /// <summary>
        /// Navigates to the Register ViewModel.
        /// </summary>
        public ICommand NavigateToRegisterCommand => new RelayCommand(NavigateToRegister, CanExecute);

        /// <summary>
        /// Attempts to log the user in.
        /// </summary>
        public ICommand LoginCommand => new RelayCommand(LoginInDatabase, CanExecute);
        #endregion
    }
}
