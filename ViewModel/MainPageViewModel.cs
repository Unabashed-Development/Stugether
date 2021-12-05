using System.Collections.ObjectModel;
using ViewModel.Commands;

namespace ViewModel
{
    /// <summary>
    /// ViewModel for MainPage
    /// </summary>
    public class MainPageViewModel : ObservableObject
    {
        #region Properties
        /// <summary>
        /// Collection of pages for the main navigation menu
        /// </summary>
        public ObservableCollection<MainMenuNavigationItemData> MainNavigationItems { get; } = new ObservableCollection<MainMenuNavigationItemData>()
        {
            new MainMenuNavigationItemData("Home", "HomePage.xaml", null),
            new MainMenuNavigationItemData("Profile", "ProfilePage.xaml", null),
            new MainMenuNavigationItemData("Hobby opties", "HobbyOptionsView.xaml", null)
        };


        private string currentVisiblePage = "HomePage.xaml";
        /// <summary>
        /// Page that is currently visible on the frame
        /// </summary>
        public string CurrentVisiblePage
        {
            get
            {
                return currentVisiblePage;
            }
            set
            {
                currentVisiblePage = value;
                RaisePropertyChanged("CurrentVisiblePage");
            }
        }

        /// <summary>
        /// Handles the click events of the main menu buttons
        /// </summary>
        public NavigateButtonCommand NavigateButtonCommand { get; set; }
        #endregion

        #region constructor
        /// <summary>
        /// Creates a new viewmodel for MainPage
        /// </summary>
        public MainPageViewModel()
        {
            NavigateButtonCommand = new NavigateButtonCommand(this);
        }
        #endregion

        #region MainMenuNavigationItemData
        /// <summary>
        /// The data for a menu item on the main navigation menu
        /// </summary>
        public struct MainMenuNavigationItemData
        {
            /// <summary>
            /// Creates an item of data for the main navigation menu
            /// </summary>
            /// <param name="title">The title of the page shown on the top of the main page</param>
            /// <param name="page">The page to be navigated to</param>
            /// <param name="extraInformation">Optional extra information to be given when navigating</param>
            public MainMenuNavigationItemData(string title, string page, object extraInformation)
            {
                Title = title;
                Page = page;
                ExtraInformation = extraInformation;
            }

            /// <summary>
            /// The title of the page shown on the top of the main page
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// The page to be navigated to
            /// </summary>
            public string Page { get; set; }
            /// <summary>
            /// Optional extra information to be given when navigating
            /// </summary>
            public object ExtraInformation { get; set; }
        }
        #endregion
    }
}
