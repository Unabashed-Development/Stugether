using Gateway;
using System;
using System.Collections.Generic;
using System.Text;
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
                    if (DataAccess.CheckIfAccountExists(Email))
                    {
                        bool verified = AccountHelper.VerifyPassword(Password, DataAccess.GetHashedPassswordFromAccount(Email));
                        if (verified)
                        {
                            Account.password = AccountHelper.HashPassword(Password);
                            ErrorMessage = "(debug) Welkom!";
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

        private void NavigateToRegister() => navigationStore.CurrentViewModel = new RegisterViewModel(navigationStore);
        #endregion

        #region Construction
        public LoginViewModel(AuthenticationNavigationStore navigationStore) => base.navigationStore = navigationStore;
        #endregion

        #region Commands
        public ICommand NavigateToRegisterCommand => new RelayCommand(NavigateToRegister, CanExecute);

        public ICommand LoginCommand => new RelayCommand(LoginInDatabase, CanExecute);
        #endregion
    }
}
