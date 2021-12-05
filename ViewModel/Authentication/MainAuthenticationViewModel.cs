using Gateway;
using System;
using ViewModel.Commands;
using ViewModel.Stores;

namespace ViewModel
{
    public class MainAuthenticationViewModel : ObservableObject
    {
        #region Fields
        private readonly AuthenticationNavigationStore _navigationStore;
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
        public MainAuthenticationViewModel(AuthenticationNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
            ((AuthenticationViewModelBase)_navigationStore.CurrentViewModel).LoggedIn += OnLoggedIn;
            SSHConnection.Initialize(); // Initialize SSH for the database connection and logging in
        }
        #endregion

        #region Events
        public event Action RequestClose;
        #endregion

        #region Methods
        /// <summary>
        /// Let the view know the ViewModel has changed, so it can display the new view.
        /// </summary>
        private void OnCurrentViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
            ((AuthenticationViewModelBase)_navigationStore.CurrentViewModel).LoggedIn += OnLoggedIn;
        }

        /// <summary>
        /// Raise the RequestClose event if the user has successfully logged in.
        /// </summary>
        public void OnLoggedIn()
        {
            RequestClose?.Invoke();
        }
        #endregion
    }
}
