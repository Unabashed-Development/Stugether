using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;
using Gateway;
using ViewModel.Helpers;
using ViewModel.Commands;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Input;
using System;

namespace ViewModel
{
    public class MatchingProfilePageViewModel : ObservableObject
    {

        #region properties
        private int _selectedImage = 0;
        private Boolean LikeView = false;

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
        public ObservableCollection<Uri> Images => MatchProfiles[0].UserMedia;

        //public string FirstName
        //{
        //    get => _matchingProfile.FirstName;
        //    set
        //    {
        //        _matchingProfile.FirstName = value;
        //        OnPropertyChanged("FirstName");
        //    }
        //}
        //public string LastName
        //{
        //    get => _matchingProfile.LastName;
        //    set
        //    {
        //        _matchingProfile.LastName = value;
        //        OnPropertyChanged("LastName");
        //    }
        //}

        //public string Name => MatchProfiles[0].FirstName + " " + MatchProfiles[0].LastName;

        //public string School
        //{
        //    get => _matchingProfile.School.SchoolName;
        //    set
        //    {
        //        _matchingProfile.School.SchoolName = value;
        //        OnPropertyChanged("School");
        //    }
        //}

        //public string City
        //{
        //    get => _matchingProfile.City;
        //    set
        //    {
        //        _matchingProfile.City = value;
        //        OnPropertyChanged("City");
        //    }
        //}

        //public string Study
        //{
        //    get => _matchingProfile.School.Study;
        //    set
        //    {
        //        _matchingProfile.School.Study = value;
        //        OnPropertyChanged("Study");
        //    }
        //}

        //public string Age
        //{
        //    get => _matchingProfile.Age;
        //    set
        //    {
        //        _matchingProfile.Age = value;
        //        OnPropertyChanged("Age");
        //    }
        //}

        //public string Description
        //{
        //    get => _matchingProfile.Description;
        //    set
        //    {
        //        _matchingProfile.Description = value;
        //        OnPropertyChanged("Description");
        //    }
        //}

        //public InterestsData InterestsData
        //{
        //    get => _matchingProfile.InterestsData;
        //    set
        //    {
        //        _matchingProfile.InterestsData = value;
        //        OnPropertyChanged("InterestsData");
        //    }
        //}

        #endregion

        #region Commands

        /// <summary>
        /// Gives the Users and Matched UserID to the method LikeHandler, were after it deletes the matched UserID from the list of potential matches.
        /// </summary>
        //todo add the right relationshiptype
        public RelayCommand LikeMatchCommand => new RelayCommand(
            () =>
            {
                MatchHelper.LikeHandler(Account.UserID.Value, MatchProfiles[0].UserID, 1);
                MatchProfiles.RemoveAt(0);
                RaisePropertyChanged("SelectedImage");
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


        //private Profile _matchingProfile;

        private ObservableCollection<Profile> _matchProfiles;

        public ObservableCollection<Profile> MatchProfiles
        {
            get { return _matchProfiles; }
            set {
                _matchProfiles = value;
                RaisePropertyChanged("MatchProfiles");
            }
        }

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
            LikeView = true;
        }

        /// <summary>
        /// First, a list with profiles is being stored in var l, were after the full profile is being accessed by using the userID from those profiles.
        /// </summary>

        public MatchingProfilePageViewModel()
        {

            var l = SearchProfileDataAccess.GetProfileBasedOnRelationType(Account.UserID.Value);

            MatchProfiles = new ObservableCollection<Profile>();

            foreach (var item in l)
            {
                MatchProfiles.Add(ProfileDataAccess.LoadProfile(item.UserID));
                _matchProfiles.Last().FirstName = _matchProfiles.Last().FirstName + " " + _matchProfiles.Last().LastName;
            }





            //dummy

            //School _matchingProfileSchool = new School(42, "schoolname", "schoolcity", "study");
            //MatchProfiles[0].School = _matchingProfileSchool;
            //_matchingProfile = new Profile
            //{
            //    UserID = 420,
            //    FirstName = "Henk",
            //    LastName = "Pitjes",
            //    Age = "25 jaar",
            //    Sex = true,            
            //    Description = "Laat me raden... Je hebt al de nodige bagage. Dit hele online daten is niet wat je wilt, omdat je liever iemand in de supermarkt ontmoet. Dat begrijp ik. En eerlijk gezegd? Ik ook. Hoi, ik ben Jack. Ik lijk niet op Ryan Gosling. Ik heb geen vrijwilligerswerk gedaan in Madagaskar. En heel eerlijk? Grote vissen vind té eng om mee te poseren. Wat ik wil zeggen is… dat ik niet perfect ben. En dat verwacht ik ook niet van jou. Wat ik wel ben? Doordeweeks ga ik door het leven als datingconsulent. Het geeft me een fijn gevoel om te zien hoe eenzame mensen veranderen in stralende levensgenieters. Mensen vragen me weleens wat ik doe.En dan zeg ik: “Ik voorspel de toekomst“. Als datingconsulent heb ik geen glazen bol nodig. Ik leg foto’s van mensen naast elkaar op tafel, geen tarot kaarten.Maar wat ik van maandag tot vrijdag vooral doe, is uitkijken naar zaterdag en zondag. Want dan gaan de serieuze kleren uit en spring ik in het diepe. Letterlijk, want ik zwem graag. Het liefst met een duikfles op mijn rug om de verloren schatten van de Rijn te ontdekken. Tot nu toe zijn het alleen halve fietswrakken geweest en verroeste auto-onderdelen, maar hey… ik geef de moed niet op. Een andere schat waar ik naar op zoek ben is intelligent, creatief en een familiemens. Klinkt dat als jou? Ik geef eerlijk toe dat ik soms met een beetje geluk de toekomst van anderen kan voorspellen, maar ik ben geen helderziende.Dus, als je denkt dat wij een klik kunnen hebben… Stuur me een bericht!",
            //    School = new School(420, "Windesheim", "Zwolle", "HBO-ICT"),
            //    City = "Leeuwarden",

            //};
        }

        
        //private void OnPropertyChanged(string property = null)
        //{
        //    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        //}

    }
}
