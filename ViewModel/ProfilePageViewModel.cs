using System;
using System.ComponentModel;
using Model;
namespace ViewModel
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        public string Name
        {
            get => _student.Name;
            set
            {
                _student.Name = value;
                OnPropertyChanged("Name");
            }
        }

        public string School
        {
            get => _student.School.Name;
            set
            {
                _student.School.Name = value;
                OnPropertyChanged("School");
            }
        }

        public string City
        {
            get => _student.City;
            set
            {
                _student.City = value;
                OnPropertyChanged("Address");
            }
        }

        public string Study
        {
            get => _student.School.Study;
            set
            {
                _student.School.Study = value;
                OnPropertyChanged("Study");
            }
        }

        public string Age
        {
            get => _student.Age;
            set
            {
                _student.Age = value;
                OnPropertyChanged("Age");
            }
        }

        public string Description
        {
            get => _student.Profile.Description;
            set
            {
                _student.Profile.Description = value;
                OnPropertyChanged("Description");
            }
        }

        private Student _student;

        public event PropertyChangedEventHandler PropertyChanged;

        public ProfilePageViewModel()
        {
            _student = new Student
            {
                Name = "Henk Pitjes",
                School = new School("Windesheim", "Zwolle", "HBO-ICT"),
                Age = "25 jaar",
                City = "Leeuwarden",
                Email = "Foo@bar.com",
                ID = 1,
                Profile = new Profile(_student)
            };
            _student.Profile.Description = "Laat me raden... Je hebt al de nodige bagage. Dit hele online daten is niet wat je wilt, omdat je liever iemand in de supermarkt ontmoet. Dat begrijp ik. En eerlijk gezegd? Ik ook. Hoi, ik ben Jack. Ik lijk niet op Ryan Gosling. Ik heb geen vrijwilligerswerk gedaan in Madagaskar. En heel eerlijk? Grote vissen vind té eng om mee te poseren. Wat ik wil zeggen is… dat ik niet perfect ben. En dat verwacht ik ook niet van jou. Wat ik wel ben? Doordeweeks ga ik door het leven als datingconsulent. Het geeft me een fijn gevoel om te zien hoe eenzame mensen veranderen in stralende levensgenieters. Mensen vragen me weleens wat ik doe.En dan zeg ik: “Ik voorspel de toekomst“. Als datingconsulent heb ik geen glazen bol nodig. Ik leg foto’s van mensen naast elkaar op tafel, geen tarot kaarten.Maar wat ik van maandag tot vrijdag vooral doe, is uitkijken naar zaterdag en zondag. Want dan gaan de serieuze kleren uit en spring ik in het diepe. Letterlijk, want ik zwem graag. Het liefst met een duikfles op mijn rug om de verloren schatten van de Rijn te ontdekken. Tot nu toe zijn het alleen halve fietswrakken geweest en verroeste auto-onderdelen, maar hey… ik geef de moed niet op. Een andere schat waar ik naar op zoek ben is intelligent, creatief en een familiemens. Klinkt dat als jou? Ik geef eerlijk toe dat ik soms met een beetje geluk de toekomst van anderen kan voorspellen, maar ik ben geen helderziende.Dus, als je denkt dat wij een klik kunnen hebben… Stuur me een bericht!"; //get description from db
        }

        private void OnPropertyChanged(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
