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
        private int _passwordStrength;
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
                PasswordStrength = (int)PasswordHelper.GetPasswordStrength(Account.Password);
                // Get the password strength when password is getting written
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

        public int PasswordStrength
        {
            get => _passwordStrength;
            set
            {
                _passwordStrength = value;
                RaisePropertyChanged("PasswordStrength");
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
            if (Account.Email != null && Account.Password != null && Account.Email.Length > 0 && Account.Password.Length > 0)
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
            else
            {
                ErrorMessage = "Niet alle velden zijn ingevuld.";
            }
        }

        /// <summary>
        /// Makes an account for the user in database. Sets error messages if something is wrong.
        /// </summary>
        private void CreateAccountInDatabase()
        {
            if (Account.Email != null && Account.Password != null && Account.Email.Length > 0 && Account.Password.Length > 0)
            {
                if (AccountHelper.IsValidEmail(Email) && AccountHelper.IsSchoolEmail(Email))
                {
                    if (PasswordHelper.IsStrongPassword(Password))
                    {
                        Account.Password = AccountHelper.HashPassword(Password); // To prepare, hash the password
                        if (!DataAccess.CheckIfAccountExists(Account)) // This method makes use of the last preparation
                        {
                            string verificationCode = AccountHelper.GenerateVerificationCode(Email);
                            DataAccess.CreateAccount(Account, verificationCode);
                            EmailService.SendVerificationMail(Account, verificationCode);
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
                    ErrorMessage = "Dit e-mailadres is geen geldig school adres.";
                }
            }
            else
            {
                ErrorMessage = "Niet alle velden zijn ingevuld.";
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