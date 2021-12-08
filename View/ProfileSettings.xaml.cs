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

namespace View
{
    /// <summary>
    /// Interaction logic for ProfileSettings.xaml
    /// </summary>
    public partial class ProfileSettings : Page
    {
        public ProfileSettings()
        {
            InitializeComponent();
        }

        // TODO: Can this be MVVM'd?
        private void AddPhoto_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new Microsoft.Win32.OpenFileDialog();
            dialog.Filter = "Foto's (.jpg, .png)|*.jpg;*.jpeg;*.png";

            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                ((sender as Button).DataContext as ViewModel.ProfilePagePhotosViewModel).SelectedMediaFileForUpload = dialog.FileName;
            }
        }
    }
}
