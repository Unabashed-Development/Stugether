using Model;
using System;
using ViewModel.Stores;

namespace ViewModel.Mediators
{
    public static class ViewModelMediators
    {
        ///// <summary>
        ///// This mediator serves as a way to subscribe to the FinishLoggingIn event of the MainAuthenticationViewModel.
        ///// </summary>
        //public static MainAuthenticationViewModel CurrentAuthenticationViewModel { get; set; }

        public static event Action UserAuthenticated;

        public static bool Authenticated
        {
            get => Account.Authenticated;
            set
            {
                Account.Authenticated = value;
                UserAuthenticated?.Invoke();
            }
        }
    }
}
