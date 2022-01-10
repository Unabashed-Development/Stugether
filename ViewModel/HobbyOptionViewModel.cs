using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Model;

namespace ViewModel
{
    public class HobbyOptionViewModel : INotifyPropertyChanged
    {

        public ObservableCollection<HobbyOption> HobbyOptions { get; set; }




        public HobbyOptionViewModel()
        {
            List<HobbyOption> lijstje = new List<HobbyOption>()
            {
                new HobbyOption() { Name = "hobby1" , IsChecked = false},
                new HobbyOption() { Name = "hobby2", IsChecked = true },
                new HobbyOption() { Name = "hobby3", IsChecked = false },
                new HobbyOption() { Name = "hobby4" ,IsChecked = false},
                new HobbyOption() { Name = "hobby5" ,IsChecked = false},
            };

            HobbyOptions = new ObservableCollection<HobbyOption>(lijstje);
        }


        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
