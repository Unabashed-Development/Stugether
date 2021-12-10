using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Helpers;
using ViewModel.Mediators;

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
