using Gateway;
using Model;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// Interaction logic for ProfileSettings.xaml
    /// </summary>
    public partial class ProfileSettings : Page
    {

        #region fields
        private string _currentTab { get; set; }
        #endregion

        #region constructors
        public ProfileSettings()
        {
            InitializeComponent();
            _currentTab = "Persoonlijk";
        }
        #endregion

        #region methods

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

        //TODO: MVVM, return if action is successful
        /// <summary>
        /// Saves the data of the current selected tab to the database
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            //get current profile
            Profile currentProfile = Profile.LoggedInProfile;

            //do action according to the right tab
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
                case "Moraal":
                    ProfileDataAccess.UpdateMoralsData(currentProfile.MoralsData);
                    break;
                case "QA":
                    ProfileDataAccess.UpdateQaData(currentProfile.QAData);
                    break;
                default:
                    break;
            }

        }

        /// <summary>
        /// Sets the current active settings tab
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TabControl_Selected(object sender, RoutedEventArgs e)
        {
            TabItem tabItem = (TabItem)sender;
            _currentTab = tabItem.Header.ToString();
        }

        #endregion
    }
}
