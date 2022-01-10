using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ViewModel.Commands;
using ViewModel.Helpers;

namespace ViewModel
{
    /// <summary>
    /// ViewModel class for the profilesettings page according to MVVM
    /// </summary>
    public class SettingsPageViewModel : ObservableObject
    {
        #region Fields
        private Profile _student;
        #endregion

        #region Properties
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

        public string Name => _student.FirstName + " " + _student.LastName;

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

        public FullyObservableCollection<InterestChosenHelper> ChosenInterests { get; set; }

        public ObservableCollection<QA> QAList { get; } = new ObservableCollection<QA>(ProfileDataAccess.LoadAllQAData().QAList);

        public FullyObservableCollection<QAHelper> AnsweredQA { get; set; }
        #endregion

        #region Construction
        public SettingsPageViewModel()
        {
            _student = Profile.LoggedInProfile;
            LoadInterests();
            LoadQA();
        }
        #endregion

        #region Methods

        private void LoadInterests()
        {
            //add all the interests to a list
            List<InterestChosenHelper> chosenInterestsList = new List<InterestChosenHelper>();
            foreach (Interest interest in InterestsList)
            {
                chosenInterestsList.Add(new InterestChosenHelper() { Chosen = InterestsData.Interests.Contains(interest), Interest = interest });
            }
            ChosenInterests = new FullyObservableCollection<InterestChosenHelper>(chosenInterestsList);
            
            //add action listener
            ChosenInterests.ItemPropertyChanged += ChosenInterests_CollectionChanged;
        }

        private void LoadQA()
        {
            //add all the qa questions to a list
            List<QAHelper> qaHelperList = new List<QAHelper>();

            //adds all the answered qa questions to the list
            QAData.QAList.ForEach(qa => qaHelperList.Add(new QAHelper() { Answer = qa.QaAnswer, Qa = qa }));

            //adds the unanswered qa questions to the list
            foreach (QA qa in QAList)
            {
                if (!QAData.QAList.Contains(qa))
                {
                    qaHelperList.Add(new QAHelper() { Answer = "", Qa = qa });
                }
            }
            AnsweredQA = new FullyObservableCollection<QAHelper>(qaHelperList);

            //add action listener
            AnsweredQA.ItemPropertyChanged += AnsweredQA_CollectionChanged;
        }

        /// <summary>
        /// Translates the AnsweredQA to a QA object. The QA object is added to the current profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void AnsweredQA_CollectionChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            //clear the qa list first
            _student.QAData.QAList.Clear();

            //loop through all the answered QA, if its answered (not empty) the qa will be added to the profile
            IEnumerator<QAHelper> enumerator = AnsweredQA.GetEnumerator();
            while (enumerator.MoveNext())
            {
                QAHelper qaHelper = enumerator.Current;
                if (qaHelper.Answer.Equals(""))
                {
                    continue; //dont want to add unanswered questions to the list
                }
                _student.QAData.QAList.Add(qaHelper.Qa);
            }
        }

        /// <summary>
        /// Translates the ChosenInterests to a Interest object. The Interest object is added to the current profile
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void ChosenInterests_CollectionChanged(object sender, ItemPropertyChangedEventArgs e)
        {
            //clear the interests first
            _student.InterestsData.Interests.Clear();

            //loop through all the chosenInterests, if its chosen (selected) the interest will be added to the profile
            IEnumerator<InterestChosenHelper> enumerator = ChosenInterests.GetEnumerator();
            while (enumerator.MoveNext())
            {
                InterestChosenHelper interest = enumerator.Current;
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
