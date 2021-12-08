using Gateway;
using Model;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace ViewModel
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private Profile _student { set; get; }
        public ObservableCollection<string> InterestsList { get; } = new ObservableCollection<string>(ProfileDataAccess.LoadAllInterests());

        public SettingsPageViewModel()
        {
            /*            _student = new Student
                        {
                            FirstName = "Henk",
                            LastName = "Pitjes",
                            Name = "Henk Pitjes",
                            School = new School("Windesheim", "Zwolle", "HBO-ICT"),
                            Age = "25 jaar",
                            City = "Leeuwarden",
                            Email = "Foo@bar.com",
                            ID = 1,
                            Sex = true,
                            Description = "Laat me raden... Je hebt al de nodige bagage. Dit hele online daten is niet wat je wilt, omdat je liever iemand in de supermarkt ontmoet. Dat begrijp ik. En eerlijk gezegd? Ik ook. Hoi, ik ben Jack. Ik lijk niet op Ryan Gosling. Ik heb geen vrijwilligerswerk gedaan in Madagaskar. En heel eerlijk? Grote vissen vind té eng om mee te poseren. Wat ik wil zeggen is… dat ik niet perfect ben. En dat verwacht ik ook niet van jou. Wat ik wel ben? Doordeweeks ga ik door het leven als datingconsulent. Het geeft me een fijn gevoel om te zien hoe eenzame mensen veranderen in stralende levensgenieters. Mensen vragen me weleens wat ik doe.En dan zeg ik: “Ik voorspel de toekomst“. Als datingconsulent heb ik geen glazen bol nodig. Ik leg foto’s van mensen naast elkaar op tafel, geen tarot kaarten.Maar wat ik van maandag tot vrijdag vooral doe, is uitkijken naar zaterdag en zondag. Want dan gaan de serieuze kleren uit en spring ik in het diepe. Letterlijk, want ik zwem graag. Het liefst met een duikfles op mijn rug om de verloren schatten van de Rijn te ontdekken. Tot nu toe zijn het alleen halve fietswrakken geweest en verroeste auto-onderdelen, maar hey… ik geef de moed niet op. Een andere schat waar ik naar op zoek ben is intelligent, creatief en een familiemens. Klinkt dat als jou? Ik geef eerlijk toe dat ik soms met een beetje geluk de toekomst van anderen kan voorspellen, maar ik ben geen helderziende.Dus, als je denkt dat wij een klik kunnen hebben… Stuur me een bericht!"; //get description from db
                    };*/
        }

        public string FirstName
        {
            get => _student.FirstName;
            set
            {
                _student.FirstName = value;
                OnPropertyChanged("FirstName");
            }
        }

        public string LastName
        {
            get => _student.LastName;
            set
            {
                _student.LastName = value;
                OnPropertyChanged("LastName");
            }
        }

        public string City
        {
            get => _student.City;
            set
            {
                _student.City = value;
                OnPropertyChanged("City");
            }
        }

        public DateTime DateOfBirth
        {
            get => _student.DateOfBirth;
            set
            {
                _student.DateOfBirth = value;
                OnPropertyChanged("DateOfBirth");
            }
        }

        public bool Sex
        {
            get => _student.Sex;
            set
            {
                _student.Sex = value;
                OnPropertyChanged("Sex");
            }
        }

        public string SchoolName
        {
            get => _student.School.Name;
            set
            {
                _student.School.Name = value;
                OnPropertyChanged("SchoolName");
            }
        }

        public string Study
        {
            get => _student.School.Study;
            set
            {
                _student.School.Study = value;
                OnPropertyChanged("SchoolStudy");
            }
        }

        public string SchoolCity
        {
            get => _student.School.City;
            set
            {
                _student.School.City = value;
                OnPropertyChanged("SchoolCity");
            }
        }

        public string Description
        {
            get => _student.Description;
            set
            {
                _student.Description = value;
                OnPropertyChanged("Description");
            }
        }

        public MoralsData MoralsData
        {
            get => _student.MoralsData;
            set
            {
                _student.MoralsData = value;
                OnPropertyChanged("MoralsData");
            }
        }

        public QAData QAData
        {
            get => _student.QAData;
            set
            {
                _student.QAData = value;
                OnPropertyChanged("QAData");
            }
        }

        public InterestsData InterestsData
        {
            get => _student.InterestsData;
            set
            {
                _student.InterestsData = value;
                OnPropertyChanged("InterestsData");
            }
        }

        private void OnPropertyChanged(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
