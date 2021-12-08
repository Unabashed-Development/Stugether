using Gateway;
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

        /// <summary>
        /// Sets the CurrentViewModel of the navigationStore to the verification ViewModel.
        /// </summary>
        protected void NavigateToVerification() => navigationStore.CurrentViewModel = new VerificationViewModel(navigationStore);

        /// <summary>
        /// Sets the CurrentViewModel of the navigationStore to the verification ViewModel. (overload)
        /// </summary>
        /// <param name="errorMessage">Optional parameter to give the ViewModel an error message when creating it.</param>
        protected void NavigateToVerification(string errorMessage) => navigationStore.CurrentViewModel = new VerificationViewModel(navigationStore, errorMessage);
        #endregion

        #region Methods
        /// <summary>
        /// Cleans up account data not needed anymore in the application after registering or logging in.
        /// </summary>
        public void CleanUpAccountData()
        {
            Password = null;
            VerifyPassword = null;
            VerificationCode = null;
            PasswordStrength = null;
        }
        /// <summary>
        /// Logs the user in by setting the authentication and user ID to the correct value and calling OnLoggedIn to invoke an event.
        /// </summary>
        protected void LogUserIn()
        {
            Account.authenticated = true; // Set the authentication state of the application to true
            Account.userID = AccountDataAccess.GetUserIDFromAccount(Email); // Get the user ID from the account and save it in the application
            OnLoggedIn();
        }

        /// <summary>
        /// Sets the ErrorMessage property to indicate that not all fields are occupied.
        /// </summary>
        protected void ErrorMessage_NotAllFieldsOccupied() => ErrorMessage = "Niet alle velden zijn ingevuld.";

        /// <summary>
        /// Invokes the LoggedIn event to indicate that the user has logged in.
        /// </summary>
        protected void OnLoggedIn()
        {
            LoggedIn?.Invoke();
        }
        #endregion
    }
}