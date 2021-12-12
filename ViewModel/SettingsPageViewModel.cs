using Gateway;
using Model;
using System;
using System.Collections.Generic;
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
        public struct InterestChosen : INotifyPropertyChanged
        {
            private bool _chosen;

            public bool Chosen
            { 
                get => _chosen; 
                set 
                {
                    _chosen = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Chosen"));
                }
            }
            public Interest Interest { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;
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

        public TrulyObservableCollection<InterestChosen> ChosenInterests { get; set; }
        #endregion

        #region Construction
        public SettingsPageViewModel()
        {
            _student = Profile.LoggedInProfile;
            ChosenInterests = new TrulyObservableCollection<InterestChosen>();
            foreach (Interest interest in InterestsList)
            {
                ChosenInterests.Add(new InterestChosen() { Chosen = InterestsData.Interests.Contains(interest), Interest = interest });
            }
            ChosenInterests.CollectionChanged += ChosenInterests_CollectionChanged;
        }
        #endregion

        #region Methods
        public void ChosenInterests_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            _student.InterestsData.Interests.Clear();
            IEnumerator<InterestChosen> enumerator = ChosenInterests.GetEnumerator();
            while (enumerator.MoveNext())
            {
                InterestChosen interest = enumerator.Current;
                if (!interest.Chosen)
                {
                    continue;
                }

                _student.InterestsData.Interests.Add(interest.Interest);
            }
        }
        #endregion
    }
}
