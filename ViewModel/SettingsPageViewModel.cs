using Gateway;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private Profile _student { set; get; }
        public ObservableCollection<Interest> InterestsList { get; } = new ObservableCollection<Interest>(ProfileDataAccess.LoadAllInterests());

        public SettingsPageViewModel()
        {
            _student = ProfileDataAccess.LoadProfile(3);
        }

        public string FirstName
        {
            get => _student.FirstName;
            set
            {
                _student.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _student.LastName;
            set
            {
                _student.LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Name
        {
            get => _student.FirstName + " " + _student.LastName;
        }

        public string City
        {
            get => _student.City;
            set
            {
                _student.City = value;
                OnPropertyChanged("City");
            }
        }

        public DateTime DateOfBirth
        {
            get => _student.DateOfBirth;
            set
            {
                _student.DateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public bool Sex
        {
            get => _student.Sex;
            set
            {
                _student.Sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public string SchoolName
        {
            get => _student.School.SchoolName;
            set
            {
                _student.School.SchoolName = value;
                OnPropertyChanged("SchoolName");
            }
        }

        public string Study
        {
            get => _student.School.Study;
            set
            {
                _student.School.Study = value;
                OnPropertyChanged("SchoolStudy");
            }
        }

        public string SchoolCity
        {
            get => _student.School.SchoolCity;
            set
            {
                _student.School.SchoolCity = value;
                OnPropertyChanged("SchoolCity");
            }
        }

        public string Description
        {
            get => _student.Description;
            set
            {
                _student.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public MoralsData MoralsData
        {
            get => _student.MoralsData;
            set
            {
                _student.MoralsData = value;
                OnPropertyChanged("MoralsData");
            }
        }

        public QAData QAData
        {
            get => _student.QAData;
            set
            {
                _student.QAData = value;
                OnPropertyChanged("QAData");
            }
        }

        public InterestsData InterestsData
        {
            get => _student.InterestsData;
            set
            {
                _student.InterestsData = value;
                OnPropertyChanged("InterestsData");
            }
        }

        private void OnPropertyChanged(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
