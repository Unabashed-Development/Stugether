using Gateway;
using Model;
using System.Windows.Input;
using ViewModel.Commands;

namespace ViewModel
{
    public class LoginViewModel : ObservableObject
    {
        #region Fields
        private Account _account;
        #endregion

        #region Properties
        public Account Account
        {
            get { return _account; }
            set { _account = value; }
        }

        public string Email
        {
            get { return Account.Email; }
            set
            {
                Account.Email = value;
                RaisePropertyChanged("Email");
            }
        }

        public string Password
        {
            get { return Account.Password; }
            set
            {
                Account.Password = value;
                RaisePropertyChanged("Password");
            }
        }
        #endregion

        #region Construction
        public LoginViewModel()
        {
            _account = new Account();
        }
        #endregion

        #region Commands
        private void VerifyWithDatabase()
        {

            DataAccess.CreateAccount(Account);
        }

        private bool CanLogin()
        {
            return true;
        }

        public ICommand LoginCommand => new RelayCommand(VerifyWithDatabase, CanLogin);
        #endregion
    }
}
