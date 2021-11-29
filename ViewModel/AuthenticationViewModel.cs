using Gateway;
using Model;
using System;
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
        private string _verificationCode;
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

        #region Events
        public event EventHandler VerificationSent;
        #endregion

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
        public string VerificationCode
        {
            get => _verificationCode;
            set
            {
                _verificationCode = value;
                RaisePropertyChanged("VerificationCode");
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
        private void LoginInDatabase()
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

        private void VerificationOnDatabase()
        {
            if (DataAccess.CheckIfVerificationCodeMatches(_verificationCode, Account))
            {
                ErrorMessage = "(debug) Welkom bij StudentMatcher!";
            }
            else
            {
                ErrorMessage = "De verificatie code klopt niet.";
            }
        }

        /// <summary>
        /// Checks if the command can be executed.
        /// </summary>
        /// <returns>True.</returns>
        private bool CanExecute()
        {
            return true;
        }

        public ICommand LoginCommand => new RelayCommand(LoginInDatabase, CanExecute);
        public ICommand RegisterCommand => new RelayCommand(CreateAccountInDatabase, CanExecute);
        public ICommand VerifyCommand => new RelayCommand(VerificationOnDatabase, CanExecute);
        #endregion
    }
}