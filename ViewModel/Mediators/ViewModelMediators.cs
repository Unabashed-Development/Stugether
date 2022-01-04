using Model;
using System;

namespace ViewModel.Mediators
{
    public static class ViewModelMediators
    {
        #region Fields
        private static string _mainWindowPage;
        #endregion

        #region Events
        public static event Action AuthenticationStateChanged;
        public static event Action MainWindowPageChanged;
        #endregion

        #region Properties
        public static bool Authenticated
        {
            get => Account.Authenticated;
            set
            {
                Account.Authenticated = value;
                AuthenticationStateChanged?.Invoke();
            }
        }

        public static string MainWindowPage
        {
            get => _mainWindowPage;
            set
            {
                _mainWindowPage = value;
                MainWindowPageChanged?.Invoke();
            }
        }
        #endregion
    }
}
