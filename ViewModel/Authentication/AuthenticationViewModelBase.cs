using Model;
using System;
using ViewModel.Commands;
using ViewModel.Helpers;
using ViewModel.Stores;

namespace ViewModel
{
    public abstract class AuthenticationViewModelBase : ObservableObject
    {
        #region Fields
        protected AuthenticationNavigationStore navigationStore;
        private string _errorMessage;
        #endregion

        #region Properties
        public string Email
        {
            get => Account.email;
            set
            {
                Account.email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
            get => Account.password;
            set
            {
                Account.password = value;
                PasswordStrength = (int)PasswordHelper.GetPasswordStrength(Account.password);
                // Get the password strength when password is getting written
                RaisePropertyChanged("Password");
            }
        }

        public string VerifyPassword
        {
            get => Account.verifyPassword;
            set
            {
                Account.verifyPassword = value;
                RaisePropertyChanged("VerifyPassword");
            }
        }

        public int? PasswordStrength
        {
            get => Account.passwordStrength;
            set
            {
                Account.passwordStrength = value;
                RaisePropertyChanged("PasswordStrength");
            }
        }

        public string VerificationCode
        {
            get => Account.verificationCode;
            set
            {
                Account.verificationCode = value;
                RaisePropertyChanged("VerificationCode");
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                RaisePropertyChanged("ErrorMessage");
            }
        }
        #endregion

        #region Events
        public event Action LoggedIn;
        #endregion

        #region Commands
        /// <summary>
        /// Checks if the command can be executed.
        /// </summary>
        /// <returns>True.</returns>
        protected bool CanExecute()
        {
            return true;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Cleans up account data not needed anymore in the application after registering.
        /// </summary>
        protected void CleanUpAccountData()
        {
            Password = null;
            VerificationCode = null;
            PasswordStrength = null;
        }

        protected void OnLoggedIn()
        {
            LoggedIn?.Invoke();
        }
        #endregion
    }
}