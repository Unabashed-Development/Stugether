using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ViewModel
{
    public class SettingsPageViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;
        private Student Student;

        public SettingsPageViewModel()
        {

        }

        //voornaam
        //achternaam
        //woonplaats
        //geboortedatum
        //geslacht

        //school naam
        //opleiding
        //plaats school

        //beschrijving

        //interests

        //qa

        //normen en waarden

        private void OnPropertyChanged(string property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

    }
}
