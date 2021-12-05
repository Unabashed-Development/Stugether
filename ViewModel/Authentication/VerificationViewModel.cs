using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using ViewModel.Commands;

namespace ViewModel
{
    public class VerificationViewModel : AuthenticationViewModelBase
    {
        #region Commands
        private void VerificationOnDatabase()
        {
            if (DataAccess.CheckIfVerificationCodeMatches(VerificationCode, Email))
            {
                ErrorMessage = "(debug) Welkom bij StudentMatcher!";
            }
            else
            {
                ErrorMessage = "De verificatie code klopt niet.";
            }
        }

        public ICommand VerifyCommand => new RelayCommand(VerificationOnDatabase, CanExecute);
        #endregion
    }
}
