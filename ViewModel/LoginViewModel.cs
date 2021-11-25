using Model;
using System;
using System.Collections.Generic;
using System.Text;
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
        private void UpdateArtistNameExecute()
        {
            //++_count;
            //ArtistName = string.Format("Elvis ({0})", _count);
        }

        private bool CanLogin()
        {
            return true;
        }

        public ICommand LoginCommand => new RelayCommand(UpdateArtistNameExecute, CanLogin);
        #endregion
    }
}
