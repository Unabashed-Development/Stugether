using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Gateway;
using Model;
using ViewModel.Annotations;

namespace ViewModel
{
    public class SearchedStudentsViewModel : INotifyPropertyChanged
    {

        private Student _selectedStudent;
        public Student SelectedStudent
        {
            get { return _selectedStudent;}
            set
            {
                _selectedStudent = value;
                OnPropertyChanged("SearchedStudents");
            }
        }

        private string _noResultMessage;

        public string NoResultMessage
        {
            get { return _noResultMessage; }
            set
            {
                _noResultMessage = value;
                OnPropertyChanged("NoResultMessage");
            }
        }


        public ObservableCollection<Student>SearchedStudents { get; set; }

        public SearchedStudentsViewModel()
        {

            //tijdelijk inlog student voor verificatie
            Student LogdingStudent = new Student()
            {
                FirstName = "Henk",
            };

            LogdingStudent.Relationships = new HashSet<Relationships>();
            LogdingStudent.Relationships.Add(Relationships.Business);
            LogdingStudent.Relationships.Add(Relationships.Study);

            //SSHConnection.InitializeSsh();// TODO remove if merged to development

            //TODO if student = 0 / null return a propper message to the user 
            SearchedStudents = new ObservableCollection<Student>(SearchStudentsDataAccess.GetStudentsFromSearchRelationType(LogdingStudent));
            if (SearchedStudents.Count == 0 || SearchedStudents == null)
            {
                NoResultMessage = "Sorry we hebben niks gevonden!";
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
