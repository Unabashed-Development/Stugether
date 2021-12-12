using Gateway;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using ViewModel.Commands;

namespace ViewModel
{
    public class SettingsPageViewModel : ObservableObject
    {
        #region Fields
        private Profile _student;
        #endregion

        #region Properties
        public struct InterestChosen
        {
            public bool Chosen { get; set; }
            public Interest Interest { get; set; }
        }

        public string FirstName
        {
            get => _student.FirstName;
            set
            {
                _student.FirstName = value;
                RaisePropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _student.LastName;
            set
            {
                _student.LastName = value;
                RaisePropertyChanged("LastName");
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
                RaisePropertyChanged("City");
            }
        }

        public DateTime? DateOfBirth
        {
            get => _student.DateOfBirth;
            set
            {
                _student.DateOfBirth = value;
                RaisePropertyChanged("DateOfBirth");
            }
        }

        public bool? Sex
        {
            get => _student.Sex;
            set
            {
                _student.Sex = value;
                RaisePropertyChanged("Sex");
            }
        }

        public string SchoolName
        {
            get => _student.School.SchoolName;
            set
            {
                _student.School.SchoolName = value;
                RaisePropertyChanged("SchoolName");
            }
        }

        public string Study
        {
            get => _student.School.Study;
            set
            {
                _student.School.Study = value;
                RaisePropertyChanged("SchoolStudy");
            }
        }

        public string SchoolCity
        {
            get => _student.School.SchoolCity;
            set
            {
                _student.School.SchoolCity = value;
                RaisePropertyChanged("SchoolCity");
            }
        }

        public string Description
        {
            get => _student.Description;
            set
            {
                _student.Description = value;
                RaisePropertyChanged("Description");
            }
        }

        public MoralsData MoralsData
        {
            get => _student.MoralsData;
            set
            {
                _student.MoralsData = value;
                RaisePropertyChanged("MoralsData");
            }
        }

        public QAData QAData
        {
            get => _student.QAData;
            set
            {
                _student.QAData = value;
                RaisePropertyChanged("QAData");
            }
        }

        public InterestsData InterestsData
        {
            get => _student.InterestsData;
            set
            {
                _student.InterestsData = value;
                RaisePropertyChanged("InterestsData");
            }
        }

        public ObservableCollection<Interest> InterestsList { get; } = new ObservableCollection<Interest>(ProfileDataAccess.LoadAllInterests());

        public ObservableCollection<InterestChosen> ChosenInterests { get; internal set; } = new ObservableCollection<InterestChosen>();
        #endregion

        #region Construction
        public SettingsPageViewModel()
        {
            _student = Profile.LoggedInProfile;
            ChosenInterests = new ObservableCollection<InterestChosen>();
            //chosenInterests.CollectionChanged += ChosenInterests_CollectionChanged;
            foreach(Interest interest in InterestsList)
            {
                ChosenInterests.Add(new InterestChosen() { Chosen = InterestsData.Interests.Contains(interest), Interest = interest });
            }
        }
        #endregion

        #region Methods
        private void ChosenInterests_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            //_student.InterestsData.Interests.Clear();
        }
        #endregion
    }
}
