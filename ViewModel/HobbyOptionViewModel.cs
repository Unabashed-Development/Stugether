using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using Gateway;
using Model;
using ViewModel.Annotations;
using ViewModel.Commands;

namespace ViewModel
{
    public class HobbyOptionViewModel : INotifyPropertyChanged
    {
        #region OpgevenRelatieProperty

        private bool _isCheckedBusiness;

        public bool IsCheckedBusiness
        {
            get { return _isCheckedBusiness; }
            set
            {
                _isCheckedBusiness = value;
                OnPropertyChanged("IsCheckedBusiness");
                if (_isCheckedBusiness)
                    _student.Relationships.Add(Relationships.Business);
                else
                    _student.Relationships.Remove(Relationships.Business);
            }
        }

        private bool _isCheckedLove;

        public bool IsCheckedLove
        {
            get { return _isCheckedLove; }
            set
            {
                _isCheckedLove = value;
                OnPropertyChanged("IsCheckedLove");
                if (_isCheckedLove)
                    _student.Relationships.Add(Relationships.Love);
                else
                    _student.Relationships.Remove(Relationships.Love);
            }
        }

        private bool _isCheckedFriends;

        public bool IsCheckedFriends
        {
            get { return _isCheckedFriends; }
            set
            {
                _isCheckedFriends = value;
                OnPropertyChanged("IscheckedFriends");
                if (_isCheckedFriends)
                    _student.Relationships.Add(Relationships.Friends);
                else
                    _student.Relationships.Remove(Relationships.Friends);
            }
        }

        private bool _isCheckedStudy;

        public bool IsCheckedStudy
        {
            get { return _isCheckedStudy; }
            set
            {
                _isCheckedStudy = value;
                OnPropertyChanged("IsCheckedStudy");
                if (_isCheckedStudy)
                    _student.Relationships.Add(Relationships.Study);
                else
                    _student.Relationships.Remove(Relationships.Study);
            }
        }
        #endregion



        #region HobbyOptionsProperty
        private ObservableCollection<Hobby> _hobbyOptions;

        public ObservableCollection<Hobby> HobbyOptions
        {
            get => _hobbyOptions;
            set
            {
                _hobbyOptions = value;
                OnPropertyChanged("HobbyOptions");
            }
        }


        #endregion

        public SaveHobbyOptionCommand SaveCommand { get; set; }

        public CheckBoxCheckCommand BoxCommand { get; set; }

        private Student _student;
        public HobbyOptionViewModel()
        {
            //Dummy
            _student = new Student();
            _student.Relationships = new HashSet<Relationships>();



            SaveCommand = new SaveHobbyOptionCommand(this);
            BoxCommand = new CheckBoxCheckCommand(this);

            //Dummy
            List<Hobby> lijstje = new List<Hobby>()
            {
                new Hobby() { Name = "hobby1", IsChecked = false},
                new Hobby() { Name = "hobby2", IsChecked = true },
                new Hobby() { Name = "hobby3", IsChecked = false },
                new Hobby() { Name = "hobby4", IsChecked = false},
                new Hobby() { Name = "hobby5", IsChecked = false},
            };

            
            HobbyOptions = new ObservableCollection<Hobby>(lijstje);
            //SSHConnection.InitializeSsh();// TODO remove if merged to development


        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        public void UpdateRelationCheckBox()
        {

        }


        public void SaveOptionsToDatabase()
        {
            RelationShipDataAcces.SaveRelations(_student);
        }
    }
}
