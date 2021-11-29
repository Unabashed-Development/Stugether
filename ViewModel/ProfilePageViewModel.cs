using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Model;
namespace ViewModel
{
    public class ProfilePageViewModel : INotifyPropertyChanged
    {
        public string Name {
            get
            {
                
                return _student.Name;
            }
            set
            {
                _student.Name = value;
                OnPropertyChanged("Name");
            }
        }

        private Student _student;


        public event PropertyChangedEventHandler PropertyChanged;

        public ProfilePageViewModel()
        {


   

            _student = new Student();
            _student.Name = "";
        }

        private void OnPropertyChanged(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
