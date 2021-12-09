namespace ViewModel.Mediators
{
    public static class ViewModelMediators
    {
        /// <summary>
        /// This mediator serves as a way to subscribe to the FinishLoggingIn event of the MainAuthenticationViewModel.
        /// </summary>
        public static MainAuthenticationViewModel CurrentAuthenticationViewModel { get; set; }
    }
}
