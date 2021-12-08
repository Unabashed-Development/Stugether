using System;
using ViewModel.Commands;

// Inspiration by: https://www.youtube.com/watch?v=N26C_Cq-gAY
namespace ViewModel.Stores
{
    public class AuthenticationNavigationStore
    {
        #region Fields
        private ObservableObject _currentViewModel;
        #endregion

        #region Properties
        public ObservableObject CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnCurrentViewModelChanged();
            }
        }
        #endregion

        #region Events
        public event Action CurrentViewModelChanged;
        #endregion

        #region Methods
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
        #endregion
    }
}