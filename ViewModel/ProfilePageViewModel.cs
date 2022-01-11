using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Commands;

namespace ViewModel
{
    /// <summary>
    /// ViewModel class for the profile page according to MVVM
    /// </summary>
    public class ProfilePageViewModel : ObservableObject
    {
        #region Fields
        private Profile _profile;
        private int _selectedImage = 0;
        #endregion

        #region Properties
        public int UserID => _profile.UserID;

        public string FirstName
        {
            get => _profile.FirstName;
            set
            {
                _profile.FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => _profile.LastName;
            set
            {
                _profile.LastName = value;
                RaisePropertyChanged("LastName");
            }
        }

        public string Name => _profile.FirstName + " " + _profile.LastName;

        public string School
        {
            get => _profile.School.SchoolName;
            set
            {
                _profile.School.SchoolName = value;
                RaisePropertyChanged("School");
            }
        }

        public string City
        {
            get => _profile.City;
            set
            {
                _profile.City = value;
                RaisePropertyChanged("City");
            }
        }

        public string Study
        {
            get => _profile.School.Study;
            set
            {
                _profile.School.Study = value;
                RaisePropertyChanged("Study");
            }
        }

        public string Age
        {
            get => _profile.Age;
            set
            {
                _profile.Age = value;
                RaisePropertyChanged("Age");
            }
        }

        public string Description
        {
            get => _profile.Description;
            set
            {
                _profile.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public InterestsData InterestsData
        {
            get => _profile.InterestsData;
            set
            {
                _profile.InterestsData = value;
                RaisePropertyChanged("InterestsData");
            }
        }

        public MoralsData MoralsData
        {
            get => _profile.MoralsData;
            set
            {
                _profile.MoralsData = value;
                RaisePropertyChanged("MoralsData");
            }
        }

        public QAData QAData
        {
            get => _profile.QAData;
            set
            {
                _profile.QAData = value;
                RaisePropertyChanged("QAData");
            }
        }

        /// <summary>
        /// Gives the image index currently selected to show on the profile page
        /// </summary>
        public Uri SelectedImage
        {
            get
            {
                if (Images.Count > 0)
                {
                    if (_selectedImage > 0 && _selectedImage < Images.Count)
                    {
                        return Images[_selectedImage];
                    }
                    else
                        if (_selectedImage < 0)
                    {
                        _selectedImage += Images.Count;
                    }
                    return Images[_selectedImage % Images.Count];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gives the list with media on the users profile
        /// </summary>
        public ObservableCollection<Uri> Images => _profile.UserMedia;
        #endregion

        #region Commands

        /// <summary>
        /// Handles the next and previous buttons for the photos on the profile page
        /// </summary>
        public ICommand PhotoNavigationButtonCommand => new RelayCommand(
            (parameter) =>
            {
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
                RaisePropertyChanged("SelectedImage");
            },
            () => true
            );
        #endregion

        #region Construction
        public ProfilePageViewModel(Profile profile)
        {
            _profile = profile;
        }

        public ProfilePageViewModel()
        {
            _profile = Profile.LoggedInProfile;
        }
        #endregion
    }
}
