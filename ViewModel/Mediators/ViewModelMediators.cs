using Model;
using System;

namespace ViewModel.Mediators
{
    public static class ViewModelMediators
    {
        #region Events
        public static event Action AuthenticationStateChanged;
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
        #endregion
    }
}
