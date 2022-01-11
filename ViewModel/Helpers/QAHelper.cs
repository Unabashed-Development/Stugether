using Model;
using System.ComponentModel;

namespace ViewModel.Helpers
{
    /// <summary>
    /// Class that sends an event when the answer of the QA is changed
    /// </summary>
    public class QAHelper : INotifyPropertyChanged
    {

        #region fields
        private string _answer;

        public string Answer
        {
            get => _answer;
            set
            {
                _answer = value;
                if (Qa != null)
                {
                    Qa.QaAnswer = _answer;
                }
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Answer")); //sends the event
            }
        }
        public QA Qa { get; set; }
        #endregion

        #region events
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

    }
}