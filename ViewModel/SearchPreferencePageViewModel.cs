using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Gateway;
using Model;
using ViewModel.Annotations;
using ViewModel.Commands;

namespace ViewModel
{
    public class SearchPreferencePageViewModel : INotifyPropertyChanged
    {

        #region Property
        public bool Business
        {
            get { return RelationType.Business; }
            set
            {
                RelationType.Business = value;
                OnPropertyChanged("Business");
                
            }
        }

        public bool Love
        {
            get { return RelationType.Love; }
            set
            {
                RelationType.Love = value;
                OnPropertyChanged("Love");
            }
        }

       

        public bool Friend
        {
            get { return RelationType.Friend; }
            set
            {
                RelationType.Friend = value;
                OnPropertyChanged("Friend");
                
            }
        }

        public bool StudyBuddy
        {
            get { return RelationType.StudyBuddy; }
            set
            {
                RelationType.StudyBuddy = value;
                OnPropertyChanged("StudyBuddy");
            }
        }


        private RelationType _relationType;

        public RelationType RelationType
        {
            get { return _relationType; }
            set
            {
                _relationType = value;
                OnPropertyChanged("RelationType");
            }
        }

        #endregion



        public RelayCommand SaveCommand => new RelayCommand(parameter =>
        {
            SearchPreferenceDataAccess.SaveRelationPreference(RelationType,(int)Account.UserID);
        }, ()=>true);
        

        


        public SearchPreferencePageViewModel()
        {
            RelationType = SearchPreferenceDataAccess.GetRelationType((int)Account.UserID);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
