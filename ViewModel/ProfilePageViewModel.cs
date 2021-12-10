using Gateway;
using Model;
using System.ComponentModel;
namespace ViewModel
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        public string FirstName
        {
            get => _profile.FirstName;
            set
            {
                _profile.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => _profile.LastName;
            set
            {
                _profile.LastName = value;
                OnPropertyChanged("LastName");
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
                OnPropertyChanged("School");
            }
        }

        public string City
        {
            get => _profile.City;
            set
            {
                _profile.City = value;
                OnPropertyChanged("City");
            }
        }

        public string Study
        {
            get => _profile.School.Study;
            set
            {
                _profile.School.Study = value;
                OnPropertyChanged("Study");
            }
        }

        public string Age
        {
            get => _profile.Age;
            set
            {
                _profile.Age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Description
        {
            get => _profile.Description;
            set
            {
                _profile.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public InterestsData InterestsData
        {
            get => _profile.InterestsData;
            set
            {
                _profile.InterestsData = value;
                OnPropertyChanged("InterestsData");
            }
        }

        private Profile _profile;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfilePageViewModel(int userID) : this(ProfileDataAccess.LoadProfile(userID)) { }

        public ProfilePageViewModel(Profile profile)
        {
            _profile = profile;
        }

        public ProfilePageViewModel()
        {
            _profile = ProfileDataAccess.LoadProfile(3);
        }

        private void OnPropertyChanged(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
