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
            LoginView loginPage = new LoginView();
            Authentication.Content = loginPage;
            loginPage.RegisterGotClicked += OnRegisterGotClicked;
        }

        /// <summary>
        /// Creates a new RegisterPage, shows the content to the frame and subscribes to the click event.
        /// </summary>
        private void InitializeRegisterView()
        {
            RegisterView registerPage = new RegisterView();
            Authentication.Content = registerPage;
            registerPage.LoginGotClicked += OnLoginGotClicked;
        }

        /// <summary>
        /// When the button for Register got clicked, initialize the RegisterPage.
        /// </summary>
        public void OnRegisterGotClicked(object sender, EventArgs e) => InitializeRegisterView();

        /// <summary>
        /// When the button for Register got clicked, initialize the LoginPage.
        /// </summary>
        public void OnLoginGotClicked(object sender, EventArgs e) => InitializeLoginView();
    }
}
