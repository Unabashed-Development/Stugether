using Gateway;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Helpers;
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
            if (Email == null || Password == null || Email.Length == 0 || Password.Length == 0)
            {
                ErrorMessage_NotAllFieldsOccupied();
                return;
            }

            if (!AccountHelper.IsValidEmail(Email))
            {
                ErrorMessage = "Dit e-mailadres is niet geldig.";
                return;
            }

            if (!AccountDataAccess.CheckIfAccountExists(Email))
            {
                ErrorMessage = "Dit account bestaat niet.";
                return;
            }

            bool passwordVerified = AccountHelper.VerifyPassword(Password, AccountDataAccess.GetHashedPassswordFromAccount(Email));
            if (!passwordVerified)
            {
                ErrorMessage = "Je inloggegevens zijn onjuist.";
                return;
            }

            CleanUpAccountData(); // Clear sensitive account data before verifying the user
            if (AccountDataAccess.CheckIfAccountIsVerified(Email))
            {
                LogUserIn();
            }
            else
            {
                ErrorMessage = "Je account is nog niet geverifieerd.";
                string verificationCode = AccountDataAccess.GetVerificationCodeFromAccount(Email); // Get the verification code from the database
                EmailService.SendVerificationMail(Email, verificationCode); // Send the user an email with the verification code
                NavigateToVerification(ErrorMessage); // Navigate to the verification code page
            }
        }

        /// <summary>
        /// Sets the CurrentViewModel of the navigationStore to the register ViewModel.
        /// </summary>
        private void NavigateToRegister() => navigationStore.CurrentViewModel = new RegisterViewModel(navigationStore);
        #endregion

        #region Construction
        public LoginViewModel(NavigationStore navigationStore) => base.navigationStore = navigationStore;
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
