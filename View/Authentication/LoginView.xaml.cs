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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace View.Authentication
{
    /// <summary>
    /// Interaction logic for LoginView.xaml
    /// </summary>
    public partial class LoginView : Page
    {
        public event EventHandler RegisterGotClicked;

        public LoginView()
        {
            InitializeComponent();
        }

        private void RegisterClick(object sender, MouseButtonEventArgs e)
        {
            RegisterGotClicked?.Invoke(this, e);
        }
    }
}
