using Model;
using System.ComponentModel;

namespace ViewModel.Helpers
{
    /// <summary>
    /// Class that sends an event when a interest button is clicked
    /// </summary>
    public class InterestChosenHelper : INotifyPropertyChanged
    {

        #region fields
        private bool _chosen;

        public bool Chosen
        {
            get => _chosen;
            set
            {
                _chosen = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Chosen")); //sends the event
            }
        }
        public Interest Interest { get; set; }
        #endregion

        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}
