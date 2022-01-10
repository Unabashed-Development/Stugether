using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;
using Gateway;
using ViewModel.Helpers;
using ViewModel.Commands;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;

namespace ViewModel
{
    public class MatchingProfilePageViewModel : ObservableObject
    {

        #region Fields
        private int _selectedImage = 0;
        private Boolean _showRelationshipTypePopup = false;
        private RelationType _isEnabledInPopup = new RelationType();
        private RelationType _outputPopup = new RelationType();
        private ObservableCollection<Profile> _matchProfiles;
        #endregion

        #region Properties
        public Boolean ShowRelationshipTypePopup
        {
            get
            {
                return _showRelationshipTypePopup;
            }
            set
            {
                _showRelationshipTypePopup = value;
                RaisePropertyChanged("ShowRelationshipTypePopup");
            }
        }

        /// <summary>
        /// Gives the image index currently selected to show on the profile page
        /// </summary>
        public Uri SelectedImage
        {
            get
            {
                if (Images.Count > 0)
                {
                    if (_selectedImage > 0 && _selectedImage < Images.Count)
                    {
                        return Images[_selectedImage];
                    }
                    else
                        if (_selectedImage < 0)
                    {
                        _selectedImage += Images.Count;
                    }
                    return Images[_selectedImage % Images.Count];
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gives the list with media on the users profile
        /// </summary>
        public ObservableCollection<Uri> Images
        {
            get
            {
                if (MatchProfiles.Count != 0)
                {
                    return MatchProfiles[0].UserMedia;
                }
                else
                {
                    ObservableCollection<Uri> UriListDummy = new ObservableCollection<Uri>();
                    Uri UriDummy = new Uri("http://www.stugether.wafoe.nl/media/671f79ae-57c3-4c21-bca2-27cee35da745.png") ;
                    UriListDummy.Add(UriDummy);
                    return UriListDummy;
                }                
            }
        }

        public bool IsEnabledLove
        {
            get { return _isEnabledInPopup.Love; }
            set
            {
                _isEnabledInPopup.Love = value;
                RaisePropertyChanged("IsEnabledLove");
            }
        }
        
        public bool IsEnabledBusiness
        {
            get { return _isEnabledInPopup.Business; }
            set
            {
                _isEnabledInPopup.Business = value;
                RaisePropertyChanged("IsEnabledBusiness");
            }
        }
        
        public bool IsEnabledStudyBuddy
        {
            get { return _isEnabledInPopup.StudyBuddy; }
            set
            {
                _isEnabledInPopup.StudyBuddy = value;
                RaisePropertyChanged("IsEnabledStudyBuddy");
            }
        }
        public bool IsEnabledFriend
        {
            get { return _isEnabledInPopup.Friend; }
            set
            {
                _isEnabledInPopup.Friend = value;
                RaisePropertyChanged("IsEnabledFriend");
            }
        }
        public bool OutputPopupLove
        {
            get { return _outputPopup.Love; }
            set
            {
                _outputPopup.Love = value;
                RaisePropertyChanged("OutputPopupLove");
                RaisePropertyChanged("LikePopup");
            }
        }
        
        public bool OutputPopupBusiness
        {
            get { return _outputPopup.Business; }
            set
            {
                _outputPopup.Business = value;
                RaisePropertyChanged("OutputPopupBusiness");
                RaisePropertyChanged("LikePopup");
            }
        }
        public bool OutputPopupStudyBuddy
        {
            get { return _outputPopup.StudyBuddy; }
            set
            {
                _outputPopup.StudyBuddy = value;
                RaisePropertyChanged("OutputPopupStudyBuddy");
                RaisePropertyChanged("LikePopup");
            }
        }

        public bool OutputPopupFriend
        {
            get { return _outputPopup.Friend; }
            set
            {
                _outputPopup.Friend = value;
                RaisePropertyChanged("OutputPopupFriend");
                RaisePropertyChanged("LikePopup");
            }
        }

        public ObservableCollection<Profile> MatchProfiles
        {
            get { return _matchProfiles; }
            set
            {
                _matchProfiles = value;
                RaisePropertyChanged("MatchProfiles");
            }
        }
        #endregion

        #region Commands

        /// <summary>
        /// Gives the Users and Matched UserID to the method LikeHandler, were after it deletes the matched UserID from the list of potential matches.
        /// </summary>        
        public RelayCommand LikeMatchCommand => new RelayCommand(
            () =>
            {

                List<int> RT = MatchHelper.RelationshipHandler(Account.UserID.Value, MatchProfiles[0].UserID);
                if (RT.Count() == 1)
                {
                    MatchHelper.LikeHandler(Account.UserID.Value, MatchProfiles[0].UserID, RT[0]);
                    MatchProfiles.RemoveAt(0);
                    RaisePropertyChanged("SelectedImage");
                }
                else
                {
                    IsEnabledInPopup(RT);
                }   
            },
            () => MatchProfiles.Count != 0);


        /// <summary>
        /// Deletes the matched userID from the list of potential matches. And adds it to the blocklist.
        /// </summary>
        public RelayCommand DislikeMatchCommand => new RelayCommand(
            () =>
            {
                BlockedDataAccess.BlockUserID(Account.UserID.Value, MatchProfiles[0].UserID, BlockReason.Disliked);
                MatchDataAccess.RemoveMatchFromUser(Account.UserID.Value, MatchProfiles[0].UserID);
                MatchProfiles.RemoveAt(0);
                RaisePropertyChanged("SelectedImage");

            },
            () => MatchProfiles.Count != 0);

        public RelayCommand ClosePopup => new RelayCommand(
            () =>
            {
                ShowRelationshipTypePopup = false;
            },
            () => true);

        public RelayCommand LikePopup => new RelayCommand(
            () =>
            {
                MatchHelper.LikeHandler(Account.UserID.Value, MatchProfiles[0].UserID, OutputPopup());
                MatchProfiles.RemoveAt(0);
                RaisePropertyChanged("SelectedImage");
                ShowRelationshipTypePopup = false;
            },
            () => CanLikePopup());

        /// <summary>
        /// Handles the next and previous buttons for the photos on the profile page
        /// </summary>
        public ICommand PhotoNavigationButtonCommand => new RelayCommand(
            (parameter) =>
            {
                if (Images.Count > 0)
                {
                    if ((string)parameter == "+")
                    {
                        _selectedImage++;
                        _selectedImage %= Images.Count;
                    }
                    else if ((string)parameter == "-")
                    {
                        _selectedImage--;
                        _selectedImage %= Images.Count;
                    }
                }
                RaisePropertyChanged("SelectedImage");
            },
            () => true
            );
        #endregion

        #region Construction
        /// <summary>
        /// The chosen profiles is shown first where after the other profiles in the liked list are shown.
        /// </summary>
        /// <param name="profile"></param>
        public MatchingProfilePageViewModel(Profile profile)
        {
            MatchProfiles = new ObservableCollection<Profile>();
            List<int> TempList = MatchDataAccess.GetReceivedLikesFromUser(Account.UserID.Value);
            
            foreach (int UserID in TempList)
            {
                if (UserID != profile.UserID){
                    MatchProfiles.Add(ProfileDataAccess.LoadProfile(UserID));
                }
            }
            MatchProfiles.Insert(0, ProfileDataAccess.LoadProfile(profile.UserID));
            _matchProfiles[0].FirstName = _matchProfiles[0].FirstName + " " + _matchProfiles[0].LastName;            
        }

        /// <summary>
        /// First, a list with profiles is being stored in var l, were after the full profile is being accessed by using the userID from those profiles.
        /// </summary>

        public MatchingProfilePageViewModel()
        {
            
            //var l = SearchProfileDataAccess.GetProfileBasedOnRelationType(Account.UserID.Value);
            var l = Helpers.Decisiontree.MainDecisionTree.GetProfilesBasedOnIntrestAndNormsAndValues(Account.UserID.Value);            



            MatchProfiles = new ObservableCollection<Profile>();

            foreach (var item in l)
            {
                MatchProfiles.Add(item);
                _matchProfiles.Last().FirstName = _matchProfiles.Last().FirstName + " " + _matchProfiles.Last().LastName;
            }         
        }
        #endregion

        #region Methods
        /// <summary>
        /// Opens the Popup and enable the radiobuttons the user can choose from.
        /// </summary>
        /// <param name="RT"></param>
        public void IsEnabledInPopup(List<int> RT)
        {
            _isEnabledInPopup = new RelationType();
            if (RT.Contains(1))
            {
                IsEnabledLove = true;
            }
            if (RT.Contains(2))
            {
                IsEnabledBusiness = true;
            }
            if (RT.Contains(3))
            {
                IsEnabledStudyBuddy = true;
            }
            if (RT.Contains(4))
            {
                IsEnabledFriend = true;
            }
            ShowRelationshipTypePopup = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public int OutputPopup()
        {
            if (OutputPopupLove == true)
            {
                return 1;
            }
            else if(OutputPopupBusiness == true)
            {
                return 2;
            }
            else if (OutputPopupStudyBuddy == true)
            {
                return 3;
            }
            else if (OutputPopupFriend == true)
            {
                return 4;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        /// Checks if of the radiobuttons has been pressed.
        /// </summary>
        /// <returns></returns>
        public Boolean CanLikePopup()
        {
            if (OutputPopup() > 0)
            {
                return true;
            }
            return false;
        }

        
        #endregion

    }
}
