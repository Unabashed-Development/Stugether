using Model;
using System.ComponentModel;

namespace ViewModel.Helpers
{
    public class InterestChosenHelper : INotifyPropertyChanged
    {
        private bool _chosen;

        public bool Chosen
        {
            get => _chosen;
            set
            {
                _chosen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Chosen"));
            }
        }
        public Interest Interest { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
