using Gateway;
using Model;
using ViewModel.Commands;
using ViewModel.Helpers;
using ViewModel.Mediators;
using ViewModel.Stores;

namespace ViewModel
{
    public abstract class AuthenticationViewModelBase : ObservableObject
    {
        #region Fields
        protected NavigationStore navigationStore;
        private string _errorMessage;
        #endregion

        #region Properties
        public string Email
        {
            get => Account.Email;
            set
            {
                Account.Email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
            get => Account.Password;
            set
            {
                Account.Password = value;
                PasswordStrength = (int)PasswordHelper.GetPasswordStrength(Account.Password);
                // Get the password strength when password is getting written
                RaisePropertyChanged("Password");
            }
        }

        public string VerifyPassword
        {
            get => Account.VerifyPassword;
            set
            {
                Account.VerifyPassword = value;
                RaisePropertyChanged("VerifyPassword");
            }
        }

        public int? PasswordStrength
        {
            get => Account.PasswordStrength;
            set
            {
                Account.PasswordStrength = value;
                RaisePropertyChanged("PasswordStrength");
            }
        }

        public string VerificationCode
        {
            get => Account.VerificationCode;
            set
            {
                Account.VerificationCode = value;
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
        /// Logs the user in by setting the authentication and user ID to the correct value and calling LoggedIn.
        /// </summary>
        protected void LogUserIn()
        {
            ViewModelMediators.Authenticated = true; // Set the authentication state of the application to true (which invokes an event)
            if (Account.UserID == null)
            {
                Account.UserID = AccountDataAccess.GetUserIDFromAccount(Email); // Get the user ID from the account and save it in the application
            }
            Profile.LoggedInProfile = ProfileDataAccess.LoadProfile(Account.UserID.Value); // Load the profile of the logged in user and set it to the static property LoggedInProfile
            ViewModelMediators.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value); // Gets the matches from the database for the logged in user
        }

        /// <summary>
        /// Sets the ErrorMessage property to indicate that not all fields are occupied.
        /// </summary>
        protected void ErrorMessage_NotAllFieldsOccupied() => ErrorMessage = "Niet alle velden zijn ingevuld.";
        #endregion
    }
}