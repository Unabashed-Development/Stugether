using Model;
using System;
using System.Collections.ObjectModel;
using ViewModel.Commands;
using ViewModel.Helpers;

namespace ViewModel
{
    public class OverviewMatchesViewModel : ObservableObject
    {
        #region Fields
        private ObservableCollection<Profile> _matches;
        #endregion

        #region Properties
        public ObservableCollection<Profile> Matches
        {
            get => _matches;
            set
            {
                _matches = value;
                RaisePropertyChanged("Matches");
            }
        }
        #endregion

        #region Construction
        public OverviewMatchesViewModel()
        {
            Account.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value);
            Matches = new ObservableCollection<Profile>(Account.Matches);
        }
        #endregion
    }
}
