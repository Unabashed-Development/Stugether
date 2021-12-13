using Gateway;
using Model;
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

        private string _currentTab { get; set; }
        public ProfileSettings()
        {
            InitializeComponent();
            _currentTab = "Persoonlijk";
        }

        // TODO: Can this be MVVM'd?
        /// <summary>
        /// Is called when the add button is clicked. This will open a OpenFileDialog to select media and puts the path in SelectedMediaFileForUpload.
        /// </summary>
        /// <param name="sender">The button which called the event. Its datacontext should be of type ViewModel.ProfilePagePhotosViewModel.</param>
        /// <param name="e">The event arguments</param>
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

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            Profile currentProfile = Profile.LoggedInProfile; //get current profile
            switch (_currentTab)
            {
                case "Persoonlijk":
                    ProfileDataAccess.UpdateProfile(currentProfile);
                    break;
                case "School":
                    ProfileDataAccess.UpdateSchool(currentProfile.School);
                    break;
                case "Interesses":

                    ProfileDataAccess.UpdateInterestsData(currentProfile.InterestsData);
                    break;
                case "Beschrijving":
                    ProfileDataAccess.UpdateProfile(currentProfile);
                    break;
                default:
                    break;
            }

        }

        private void TabControl_Selected(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = (TabItem)sender;
            _currentTab = tabItem.Header.ToString();
        }
    }
}
