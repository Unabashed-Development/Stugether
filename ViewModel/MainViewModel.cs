using ViewModel.Commands;
using ViewModel.Stores;

namespace ViewModel
{
    public class MainViewModel : ObservableObject
    {
        #region Fields
        private readonly NavigationStore _navigationStore;
        #endregion

        #region Properties
        /// <summary>
        /// Sets the CurrentViewModel to the value from the field.
        /// </summary>
        public ObservableObject CurrentViewModel => _navigationStore.CurrentViewModel;
        #endregion

        #region Construction
        /// <summary>
        /// Sets the navigation store to the given parameter and raises the event that it has changed.
        /// </summary>
        /// <param name="navigationStore">The navigation store that needs to be changed.</param>
        public MainViewModel(NavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Let the view know the ViewModel has changed, so it can display the new view. Also subscribes to the new ViewModel's LoggedIn event.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
        #endregion
    }
}
