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
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterView : Page
    {
        public event EventHandler LoginGotClicked;

        public RegisterView()
        {
            InitializeComponent();
        }

        private void LoginClick(object sender, MouseButtonEventArgs e)
        {
            LoginGotClicked?.Invoke(this, e);
        }
    }
}
