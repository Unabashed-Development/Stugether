using Gateway;
using Model;
using System.ComponentModel;
using ViewModel.Commands;

namespace ViewModel
{
    public class ProfilePageViewModel : ObservableObject
    {
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

        public string Name
        {
            get => _profile.FirstName + " " + _profile.LastName;
        }

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

        private Profile _profile;

        public ProfilePageViewModel(int userID) : this(ProfileDataAccess.LoadProfile(userID)) { }

        public ProfilePageViewModel(Profile profile)
        {
            _profile = profile;
        }

        public ProfilePageViewModel()
        {
            _profile = ProfileDataAccess.LoadProfile(3);
        }
    }
}
