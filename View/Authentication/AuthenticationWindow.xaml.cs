using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace View.Authentication
{
    /// <summary>
    /// Interaction logic for AuthenticationWindow.xaml
    /// </summary>
    public partial class AuthenticationWindow : Window
    {
        // This code can be nicer, for example: https://rachel53461.wordpress.com/2011/12/18/navigation-with-mvvm-2/
        public AuthenticationWindow()
        {
            InitializeComponent();
            InitializeLoginView();
        }
        
        /// <summary>
        /// Creates a new LoginPage, shows the content to the frame and subscribes to the click event.
        /// </summary>
        private void InitializeLoginView()
        {
            LoginView loginView = new LoginView();
            Authentication.Content = loginView;
            loginView.RegisterGotClicked += OnRegisterGotClicked;
        }

        /// <summary>
        /// Creates a new RegisterPage, shows the content to the frame and subscribes to the click event.
        /// </summary>
        private void InitializeRegisterView()
        {
            RegisterView registerView = new RegisterView();
            Authentication.Content = registerView;
            registerView.LoginGotClicked += OnLoginGotClicked;
        }

        private void InitializeVerificationView()
        {
            VerificationView verificationView = new VerificationView();
            Authentication.Content = verificationView;
            //verificationView.Registered += OnRegistered;
        }

        /// <summary>
        /// When the button for Register got clicked, initialize the RegisterPage.
        /// </summary>
        public void OnRegisterGotClicked(object sender, EventArgs e) => InitializeRegisterView();

        /// <summary>
        /// When the button for Register got clicked, initialize the LoginPage.
        /// </summary>
        public void OnLoginGotClicked(object sender, EventArgs e) => InitializeLoginView();

        /// <summary>
        /// -
        /// </summary>
        public void OnRegistered(object sender, EventArgs e) => InitializeVerificationView();
    }
}
