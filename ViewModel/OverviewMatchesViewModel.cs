using Gateway;
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
            ViewModelMediators.MatchesChanged += LoadCollectionOfMatchedProfiles;
            LoadCollectionOfMatchedProfiles();
        }

        #endregion

        #region Commands
        public ICommand UnmatchCommand => new RelayCommand((parameter) => UnmatchParameterUserID((int)parameter), () => true);
        #endregion

        #region Methods
        /// <summary>
        /// Unmatch the given userID from the logged in user, adds the given userId to the blocklist and update the ObservableCollection.
        /// </summary>
        /// <param name="userID">The ID of the user that needs to be unmatched from the logged in user.</param>
        private void UnmatchParameterUserID(int userID)
        {
            MatchDataAccess.RemoveMatchFromUser(Account.UserID.Value, userID);
            BlockedDataAccess.BlockUserID(Account.UserID.Value, userID, BlockReason.Unmatched);
            ViewModelMediators.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value); // Reload the profiles of the matches 
        }

        /// <summary>
        /// Sets the ObservableCollection for Matches.
        /// </summary>
        private void LoadCollectionOfMatchedProfiles() => Matches = new ObservableCollection<Profile>(Account.Matches);
        #endregion
    }
}
