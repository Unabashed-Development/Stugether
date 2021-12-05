using Gateway;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Helpers;
using Model;
using System;
using ViewModel.Stores;

namespace ViewModel
{
    public class RegisterViewModel : AuthenticationViewModelBase
    {
        #region Methods
        /// <summary>
        /// Makes an account for the user in database. Sets error messages if something is wrong.
        /// </summary>
        private void CreateAccountInDatabase()
        {
            if (Account.email != null && Account.password != null && Account.email.Length > 0 && Account.password.Length > 0)
            {
                if (AccountHelper.IsValidEmail(Email) && AccountHelper.IsSchoolEmail(Email))
                {
                    if (PasswordHelper.IsStrongPassword(Password))
                    {
                        Account.password = AccountHelper.HashPassword(Password); // To prepare, hash the password
                        if (!DataAccess.CheckIfAccountExists(Email)) // This method makes use of the last preparation
                        {
                            VerificationCode = AccountHelper.GenerateVerificationCode(Email);
                            DataAccess.CreateAccount(Email, Password, VerificationCode);
                            EmailService.SendVerificationMail(Email, VerificationCode);
                        }
                        else
                        {
                            ErrorMessage = "Dit account bestaat al.";
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

        private void NavigateToRegister() => navigationStore.CurrentViewModel = new LoginViewModel(navigationStore);
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
