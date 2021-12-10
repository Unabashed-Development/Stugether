using Gateway;
using Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using ViewModel.Commands;
using ViewModel.Helpers;

namespace ViewModel
{
    public class OverviewMatchesViewModel : ObservableObject
    {
        private ObservableCollection<Profile> _matches;
        public ObservableCollection<Profile> Matches
        {
            get => _matches;
            set
            {
                _matches = value;
                RaisePropertyChanged("Matches");
            }
        }

        public OverviewMatchesViewModel()
        {
            Account.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value);
            Matches = new ObservableCollection<Profile>(Account.Matches);
        }
    }
}
