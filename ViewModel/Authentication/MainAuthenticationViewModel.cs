using ViewModel.Commands;
using ViewModel.Stores;

namespace ViewModel
{
    public class MainAuthenticationViewModel : ObservableObject
    {
        private readonly AuthenticationNavigationStore _navigationStore;
        public ObservableObject CurrentViewModel => _navigationStore.CurrentViewModel;

        public MainAuthenticationViewModel(AuthenticationNavigationStore navigationStore)
        {
            _navigationStore = navigationStore;
            _navigationStore.CurrentViewModelChanged += OnCurrentViewModelChanged;
        }

        private void OnCurrentViewModelChanged()
        {
            RaisePropertyChanged(nameof(CurrentViewModel));
        }
    }
}
