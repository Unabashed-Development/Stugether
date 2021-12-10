using System;
using ViewModel.Commands;

// Inspiration by: https://www.youtube.com/watch?v=N26C_Cq-gAY
namespace ViewModel.Stores
{
    public class NavigationStore
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
        /// <summary>
        /// Let the subscribers know the current view model has changed.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            CurrentViewModelChanged?.Invoke();
        }
        #endregion
    }
}