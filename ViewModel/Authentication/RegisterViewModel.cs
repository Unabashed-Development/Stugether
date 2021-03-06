using Gateway;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Helpers;
using Model;
using ViewModel.Stores;
using System.Collections.Generic;

namespace ViewModel
{
    public class RegisterViewModel : AuthenticationViewModelBase
    {
        #region Fields
        private readonly List<string> _allowedSchoolRootDomains;
        #endregion

        #region Methods
        /// <summary>
        /// Makes an account for the user in database. Throws error messages if something is wrong.
        /// </summary>
        private void CreateAccountInDatabase()
        {
            if (Email == null || Password == null || VerifyPassword == null || Email.Length == 0 || Password.Length == 0 || VerifyPassword.Length == 0)
            {
                ErrorMessage_NotAllFieldsOccupied();
                return;
            }

            if (!AccountHelper.IsValidEmail(Email) || !AccountHelper.IsSchoolEmail(Email, _allowedSchoolRootDomains))
            {
                ErrorMessage = "Dit e-mailadres is geen geldig school adres.";
                return;
            }

            if (!PasswordHelper.IsStrongPassword(Password))
            {
                ErrorMessage = "Je wachtwoord voldoet niet aan de minimale eisen.";
                return;
            }
            if (Password != VerifyPassword)
            {
                ErrorMessage = "Je wachtwoorden komen niet overeen met elkaar.";
                return;
            }

            Account.Password = AccountHelper.HashPassword(Password); // To prepare, hash the password
            if (!AccountDataAccess.CheckIfAccountExists(Email)) // This method makes use of the last preparation
            {
                VerificationCode = AccountHelper.GenerateVerificationCode(Email); // Generate a random verification code
                AccountDataAccess.CreateAccount(Email, Password, VerificationCode); // Create the account in the database with the generated verification code
                EmailService.SendVerificationMail(Email, VerificationCode); // Send the user an email with the verification code
                Account.UserID = AccountDataAccess.GetUserIDFromAccount(Email); // After creating the account in the database, set the UserID of Account
                ProfileDataAccess.CreateEmptyProfile(Account.UserID.Value); // Create an empty profile in the database
                CleanUpAccountData(); // Clear sensitive account data before verifying the user
                NavigateToVerification(); // Navigate to the verification code page
            }
            else
            {
                ErrorMessage = "Dit account bestaat al.";
            }
        }

        /// <summary>
        /// Sets the CurrentViewModel of the navigationStore to the login ViewModel.
        /// </summary>
        private void NavigateToRegister() => navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);

        #endregion

        #region Construction
        public RegisterViewModel(NavigationStore navigationStore)
        {
            IEnumerable<AllowedSchool> _allowedSchools = AllowedSchoolsDataAccess.APICallAllowedSchools();
            _allowedSchoolRootDomains = AccountHelper.RetrieveAllDomainNamesFromAllowedSchools(_allowedSchools);
            _allowedSchoolRootDomains.Add("wafoe.nl"); // This is only for testing purposes! In a production environment, this needs to be removed.

            base.navigationStore = navigationStore;
        }
        #endregion

        #region Commands
        public ICommand NavigateToLoginCommand => new RelayCommand(NavigateToRegister, CanExecute);

        public ICommand RegisterCommand => new RelayCommand(CreateAccountInDatabase, CanExecute);
        #endregion
    }
}
