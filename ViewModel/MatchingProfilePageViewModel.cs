using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;
using Gateway;
using ViewModel.Commands;

namespace ViewModel
{
    public class MatchingProfilePageViewModel : INotifyPropertyChanged
    {

        #region oldPropery
        public string FirstName
        {
            get => _matchingProfile.FirstName;
            set
            {
                _matchingProfile.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }
        public string LastName
        {
            get => _matchingProfile.LastName;
            set
            {
                _matchingProfile.LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string Name
        {
            get => _matchingProfile.FirstName + " " + _matchingProfile.LastName;
        }

        public string School
        {
            get => _matchingProfile.School.SchoolName;
            set
            {
                _matchingProfile.School.SchoolName = value;
                OnPropertyChanged("School");
            }
        }

        public string City
        {
            get => _matchingProfile.City;
            set
            {
                _matchingProfile.City = value;
                OnPropertyChanged("City");
            }
        }

        public string Study
        {
            get => _matchingProfile.School.Study;
            set
            {
                _matchingProfile.School.Study = value;
                OnPropertyChanged("Study");
            }
        }

        public string Age
        {
            get => _matchingProfile.Age;
            set
            {
                _matchingProfile.Age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Description
        {
            get => _matchingProfile.Description;
            set
            {
                _matchingProfile.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public InterestsData InterestsData
        {
            get => _matchingProfile.InterestsData;
            set
            {
                _matchingProfile.InterestsData = value;
                OnPropertyChanged("InterestsData");
            }
        }

        #endregion

        #region Commands
        
        public RelayCommand LikeMatchCommand => new RelayCommand(
            () =>
            {
                MatchProfiles.RemoveAt(0);
            },
            () => true);

        public RelayCommand DislikeMatchCommand => new RelayCommand(
            () =>
            {
                MatchProfiles.RemoveAt(0);
            },
            () => MatchProfiles.Count != 0);
        #endregion


        private Profile _matchingProfile;


        private ObservableCollection<Profile> _matchProfiles;

        public ObservableCollection<Profile> MatchProfiles
        {
            get { return _matchProfiles; }
            set {
                _matchProfiles = value;
                OnPropertyChanged("MatchProfile");
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        public MatchingProfilePageViewModel()
        {
            //for testing
            MatchProfiles = new ObservableCollection<Profile>(SearchProfileDataAcces.GetProfileBasedOnRelationType(1));

            
            //MatchProfiles = new ObservableCollection<Profile>(SearchProfileDataAcces.GetProfileBasedOnRelationType((int)Account.UserID));


            //dummy
            _matchingProfile = new Profile
            {
                UserID = 420,
                FirstName = "Henk",
                LastName = "Pitjes",
                Age = "25 jaar",
                Sex = true,            
                Description = "Laat me raden... Je hebt al de nodige bagage. Dit hele online daten is niet wat je wilt, omdat je liever iemand in de supermarkt ontmoet. Dat begrijp ik. En eerlijk gezegd? Ik ook. Hoi, ik ben Jack. Ik lijk niet op Ryan Gosling. Ik heb geen vrijwilligerswerk gedaan in Madagaskar. En heel eerlijk? Grote vissen vind té eng om mee te poseren. Wat ik wil zeggen is… dat ik niet perfect ben. En dat verwacht ik ook niet van jou. Wat ik wel ben? Doordeweeks ga ik door het leven als datingconsulent. Het geeft me een fijn gevoel om te zien hoe eenzame mensen veranderen in stralende levensgenieters. Mensen vragen me weleens wat ik doe.En dan zeg ik: “Ik voorspel de toekomst“. Als datingconsulent heb ik geen glazen bol nodig. Ik leg foto’s van mensen naast elkaar op tafel, geen tarot kaarten.Maar wat ik van maandag tot vrijdag vooral doe, is uitkijken naar zaterdag en zondag. Want dan gaan de serieuze kleren uit en spring ik in het diepe. Letterlijk, want ik zwem graag. Het liefst met een duikfles op mijn rug om de verloren schatten van de Rijn te ontdekken. Tot nu toe zijn het alleen halve fietswrakken geweest en verroeste auto-onderdelen, maar hey… ik geef de moed niet op. Een andere schat waar ik naar op zoek ben is intelligent, creatief en een familiemens. Klinkt dat als jou? Ik geef eerlijk toe dat ik soms met een beetje geluk de toekomst van anderen kan voorspellen, maar ik ben geen helderziende.Dus, als je denkt dat wij een klik kunnen hebben… Stuur me een bericht!",
                School = new School(420, "Windesheim", "Zwolle", "HBO-ICT"),
                City = "Leeuwarden",
            
            };
        }



        private void OnPropertyChanged(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
