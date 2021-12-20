using Gateway;
using Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
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
        public OverviewMatchesViewModel() => GetMatches();
        #endregion

        #region Commands
        public ICommand UnmatchCommand => new RelayCommand((parameter) => UnmatchParameterUserID((int)parameter), () => true);
        #endregion

        #region Methods
        /// <summary>
        /// Unmatch the given userID from the logged in user and update the ObservableCollection.
        /// </summary>
        /// <param name="userID">The ID of the user that needs to be unmatched from the logged in user.</param>
        private void UnmatchParameterUserID(int userID)
        {
            MatchDataAccess.RemoveMatchFromUser(Account.UserID.Value, userID);
            GetMatches();
        }

        /// <summary>
        /// Gets the matches from the database for the logged in user and sets the ObservableCollection.
        /// </summary>
        private void GetMatches()
        {
            Account.Matches = MatchHelper.LoadProfilesOfMatches(Account.UserID.Value);
            Matches = new ObservableCollection<Profile>(Account.Matches);
        }
        #endregion
    }
}
