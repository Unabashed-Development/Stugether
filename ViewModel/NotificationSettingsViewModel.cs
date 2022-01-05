﻿using Gateway;
using Model;
using System;
using System.Windows.Input;
using ViewModel.Commands;

namespace ViewModel
{
    public class NotificationSettingsViewModel : ObservableObject
    {
        #region Fields
        private string _errorMessage;
        #endregion

        #region Properties
        public bool Matches
        {
            get => Account.NotificationSettings.Matches;
            set
            {
                Account.NotificationSettings.Matches = value;
                RaisePropertyChanged(nameof(Matches));
            }
        }

        public bool Likes
        {
            get => Account.NotificationSettings.Likes;
            set
            {
                Account.NotificationSettings.Likes = value;
                RaisePropertyChanged(nameof(Likes));
            }
        }

        public bool Chat
        {
            get => Account.NotificationSettings.Chat;
            set
            {
                Account.NotificationSettings.Chat = value;
                RaisePropertyChanged(nameof(Chat));
            }
        }

        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                _errorMessage = value;
                RaisePropertyChanged(nameof(ErrorMessage));
            }
        }
        #endregion

        #region Methods
        private void UpdateNotificationSettingsInDatabase()
        {
            try
            {
                NotificationDataAccess.SetNotificationSettings(Account.NotificationSettings, Account.UserID.Value);
                ErrorMessage = "Opslaan gelukt!";
            }
            catch (Exception)
            {
                ErrorMessage = "Er ging iets mis!";
            }
        }
        #endregion

        #region Commands
        public ICommand UpdateNotificationSettingsCommand => new RelayCommand(UpdateNotificationSettingsInDatabase, () => true);
        #endregion
    }
}
