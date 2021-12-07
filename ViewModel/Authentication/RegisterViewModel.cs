using Gateway;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Helpers;
using Model;
using ViewModel.Stores;

namespace ViewModel
{
    public class RegisterViewModel : AuthenticationViewModelBase
    {
        #region Methods
        /// <summary>
        /// Makes an account for the user in database. Throws error messages if something is wrong.
        /// </summary>
        private void CreateAccountInDatabase()
        {
            if (Email != null && Password != null && VerifyPassword != null && Email.Length > 0 && Password.Length > 0)
            {
                if (AccountHelper.IsValidEmail(Email) && AccountHelper.IsSchoolEmail(Email))
                {
                    if (PasswordHelper.IsStrongPassword(Password))
                    {
                        if (Password == VerifyPassword)
                        {
                            Account.password = AccountHelper.HashPassword(Password); // To prepare, hash the password
                            if (!AccountDataAccess.CheckIfAccountExists(Email)) // This method makes use of the last preparation
                            {
                                VerificationCode = AccountHelper.GenerateVerificationCode(Email); // Generate a random verification code
                                AccountDataAccess.CreateAccount(Email, Password, VerificationCode); // Create the account in the database with the generated verification code
                                EmailService.SendVerificationMail(Email, VerificationCode); // Send the user an email with the verification code
                                Account.userID = AccountDataAccess.GetUserIDFromAccount(Email); // Get the user ID from the account and save it in the application
                                CleanUpAccountData(); // Clear sensitive account data before verifying the user
                                NavigateToVerification(); // Navigate to the verification code page
                            }
                            else
                            {
                                ErrorMessage = "Dit account bestaat al.";
                            }
                        }
                        else
                        {
                            ErrorMessage = "Je wachtwoorden komen niet overeen met elkaar.";
                        }
                    }
                    else
                    {
                        ErrorMessage = "Je wachtwoord voldoet niet aan de minimale eisen.";
                    }
                }
                else
                {
                    ErrorMessage = "Dit e-mailadres is geen geldig school adres.";
                }
            }
            else
            {
                ErrorMessage = "Niet alle velden zijn ingevuld.";
            }
        }

        /// <summary>
        /// Sets the CurrentViewModel of the navigationStore to the login ViewModel.
        /// </summary>
        private void NavigateToRegister() => navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);

        /// <summary>
        /// Sets the CurrentViewModel of the navigationStore to the verification ViewModel.
        /// </summary>
        private void NavigateToVerification() => navigationStore.CurrentViewModel = new VerificationViewModel(navigationStore);
        #endregion

        #region Construction
        public RegisterViewModel(AuthenticationNavigationStore navigationStore) => base.navigationStore = navigationStore;
        #endregion

        #region Commands
        public ICommand NavigateToLoginCommand => new RelayCommand(NavigateToRegister, CanExecute);

        public ICommand RegisterCommand => new RelayCommand(CreateAccountInDatabase, CanExecute);
        #endregion
    }
}
