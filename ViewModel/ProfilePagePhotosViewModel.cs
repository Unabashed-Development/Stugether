using Gateway;
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
    public class ProfilePagePhotosViewModel : INotifyPropertyChanged
    {
        public Uri SelectedImage
        {
            get
            {
                if (Images.Count > 0)
                {
                    if (_selectedImage > 0 && _selectedImage < Images.Count)
                        return Images[_selectedImage];
                    else
                        return Images[_selectedImage % Images.Count];
                }
                else
                {
                    return null;
                }
            }
        }

        private int _selectedImage = 0;

        public ObservableCollection<Uri> Images { get; } = new ObservableCollection<Uri>();

        public ICommand PhotoNavigationButtonCommand => new RelayCommand(
            //(parameter) =>
            () =>
            {
                object parameter = "+"; // TODO: use given parameter when commit 63d05e29 is merged
                if (Images.Count > 0)
                {
                    if ((string)parameter == "+")
                    {
                        _selectedImage++;
                        _selectedImage %= Images.Count;
                    }
                    else if ((string)parameter == "-")
                    {
                        _selectedImage--;
                        _selectedImage %= Images.Count;
                    }
                }
                OnPropertyChanged("SelectedImage");
            },
            () => true
            );

        public ProfilePagePhotosViewModel()
        {
            List<string> imageStrings = new List<string>(MediaDataAccess.GetUserMedia(1));
            Images = new ObservableCollection<Uri>(imageStrings.ConvertAll((str) => new Uri(str, UriKind.RelativeOrAbsolute)));
        }

        #region Property change notification
        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Triggers the PropertyChanged event
        /// </summary>
        /// <param name="propertyName">The property which is changed</param>
        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}
