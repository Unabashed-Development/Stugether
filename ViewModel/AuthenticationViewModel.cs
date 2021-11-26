using Gateway;
using Model;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Helpers;

namespace ViewModel
{
    public class AuthenticationViewModel : ObservableObject
    {
        #region Fields
        private Account _account;
        private string _errorMessage;
        #endregion

        #region Properties
        public Account Account
        {
            get => _account;
            set => _account = value;
        }

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
                RaisePropertyChanged("Password");
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

        #region Construction
        public AuthenticationViewModel()
        {
            _account = new Account();
        }
        #endregion

        #region Commands
        /// <summary>
        /// Logs the user in and verifies the details with the database. Sets error messages if something is wrong.
        /// </summary>
        private void VerifyAccountWithDatabase()
        {
            if (AccountHelper.IsValidEmail(Email))
            {
                if (DataAccess.CheckIfAccountExists(Account))
                {
                    bool verified = AccountHelper.VerifyPassword(Password, DataAccess.GetHashedPassswordFromAccount(Account));
                    if (verified)
                    {
                        Account.Password = AccountHelper.HashPassword(Password);
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

        /// <summary>
        /// Makes an account for the user in database. Sets error messages if something is wrong.
        /// </summary>
        private void CreateAccountInDatabase()
        {
            if (AccountHelper.IsValidEmail(Email))
            {
                if (AccountHelper.IsSchoolEmail(Email))
                {
                    if (PasswordHelper.IsStrongPassword(Password))
                    {
                        Account.Password = AccountHelper.HashPassword(Password); // To prepare, hash the password
                        if (!DataAccess.CheckIfAccountExists(Account)) // This method makes use of the last preparation
                        {
                            DataAccess.CreateAccount(Account);
                            ErrorMessage = "(debug) Account gemaakt!";
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
                    ErrorMessage = "Dit e-mailadres is geen school adres.";
                }
            }
            else
            {
                ErrorMessage = "Dit e-mailadres is niet geldig.";
            }
        }

        /// <summary>
        /// Checks if the user can login or register.
        /// </summary>
        /// <returns>True.</returns>
        private bool CanLoginOrRegister()
        {
            return true;
        }

        public ICommand LoginCommand => new RelayCommand(VerifyAccountWithDatabase, CanLoginOrRegister);
        public ICommand RegisterCommand => new RelayCommand(CreateAccountInDatabase, CanLoginOrRegister);
        #endregion
    }
}