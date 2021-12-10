using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Input;
using ViewModel.Commands;

namespace ViewModel
{
    /// <summary>
    /// ViewModel to manage the media on the profile page
    /// </summary>
    public class ProfilePagePhotosViewModel : ObservableObject
    {
        #region Viewing images
        /// <summary>
        /// Gives the list with media on the users profile
        /// </summary>
        public ObservableCollection<Uri> Images { get; private set; } = new ObservableCollection<Uri>();

        /// <summary>
        /// Refresh the list of images on the profile
        /// </summary>
        private void RefreshImages()
        {
            List<string> imageStrings = new List<string>(MediaDataAccess.GetUserMedia(Account.UserID.Value));
            Images = new ObservableCollection<Uri>(imageStrings.ConvertAll((str) => new Uri(str, UriKind.RelativeOrAbsolute)));
            RaisePropertyChanged("Images");
        }
        #endregion

        #region Delete image
        /// <summary>
        /// Handles when the delete media button is pressed and will delete the media
        /// </summary>
        public ICommand DeletePhotoButtonCommand => new RelayCommand(
            (parameter) =>
            {
                MediaDataAccess.DeleteUserMedia(((Uri)parameter).ToString(), Account.UserID.Value);
                RefreshImages();
            },
            () => true);
        #endregion

        #region Image upload
        /// <summary>
        /// Gives path of the media picked for upload
        /// </summary>
        public string SelectedMediaFileForUpload
        {
            get => selectedMediaFileForUpload;
            set
            {
                selectedMediaFileForUpload = value;
                RaisePropertyChanged("SelectedMediaFileForUpload");
                RaisePropertyChanged("CanUpload");
            }
        }
        private string selectedMediaFileForUpload = "";

        /// <summary>
        /// Gives the user information about the upload status
        /// </summary>
        public string UploadStatus
        {
            get => uploadStatus;
            set
            {
                uploadStatus = value;
                RaisePropertyChanged("UploadStatus");
            }
        }
        private string uploadStatus = "Selecteer een bestand om te uploaden.";

        /// <summary>
        /// Handles when the upload button is pressed. The media in SelectedMediaFileForUpload will be uploaded.
        /// </summary>
        public RelayCommand UploadPhotoButtonCommand => new RelayCommand(
            () =>
            {
                UploadStatus = "Foto wordt geüpload";
                MediaDataAccess.AddUserMedia(selectedMediaFileForUpload, Account.UserID.Value);
                SelectedMediaFileForUpload = "";
                UploadStatus = "Foto is geüpload.";
                RefreshImages();
            },
            () => true);

        /// <summary>
        /// Returns if the media in SelectedMediaFileForUpload can be uploaded. It also updates UploadStatus.
        /// </summary>
        public bool CanUpload
        {
            get
            {
                if (string.IsNullOrEmpty(selectedMediaFileForUpload)) return false;
                if (string.IsNullOrWhiteSpace(selectedMediaFileForUpload)) return false;
                System.IO.FileInfo fileInfo = new System.IO.FileInfo(selectedMediaFileForUpload);
                if (!fileInfo.Exists) { UploadStatus = "Bestand bestaat niet."; return false; }
                if (fileInfo.Length > 5000000) { UploadStatus = "Bestand is te groot. Het bestand moet kleiner zijn dan 5MB."; return false; }

                UploadStatus = "Klaar om te uploaden.";
                return true;
            }
        }
        #endregion


        public ProfilePagePhotosViewModel()
        {
            RefreshImages();
        }
    }
}
