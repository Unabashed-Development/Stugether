using System;
using Gateway;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Mediators;
using System.Linq;

namespace ViewModel
{
    /// <summary>
    /// ViewModel for MainPage
    /// </summary>
    public class MainPageViewModel : ObservableObject
    {
        #region fields
        private static string homePage = @"HomePages\HomePageBeforeLogin.xaml";
        private MainMenuNavigationItemData navigationData;
        #endregion

        #region Construction
        /// <summary>
        /// Creates a new viewmodel for MainPage
        /// </summary>
        public MainPageViewModel()
        {
            SSHService.Initialize(); // Initialize SSH for the database connection and logging in
            ViewModelMediators.UserAuthenticated += OnFinishLoggingIn;
        }

        private void OnFinishLoggingIn()
        {
            var item = MainNavigationItems.FirstOrDefault(i => i.Page == homePage);
            homePage = @"HomePages\HomePageAfterLogin.xaml";
            item.Page = homePage;
            CurrentVisiblePage = homePage;
        }

        #endregion

        #region Properties
        /// <summary>
        /// Collection of pages for the main navigation menu
        /// </summary>
        public ObservableCollection<MainMenuNavigationItemData> MainNavigationItems { get; } = new ObservableCollection<MainMenuNavigationItemData>()
        {
            new MainMenuNavigationItemData("Home", homePage, null), // Make the home page dynamic
            new MainMenuNavigationItemData("Profile", "ProfilePage.xaml", null),
            new MainMenuNavigationItemData("Hobby opties", "HobbyOptionsView.xaml", null),
            new MainMenuNavigationItemData("Settings", "ProfileSettings.xaml", null)
        };

        private string currentVisiblePage = @"HomePages\HomePageBeforeLogin.xaml";
        /// <summary>
        /// Page that is currently visible on the frame
        /// </summary>
        public string CurrentVisiblePage
        {
            get => currentVisiblePage;
            set
            {
                currentVisiblePage = value;
                RaisePropertyChanged("CurrentVisiblePage");
            }
        }

        /// <summary>
        /// Handles the click events of the main menu buttons
        /// </summary>
        public ICommand NavigateButtonCommand => new RelayCommand(
            (parameter) =>
            {
                if (parameter.GetType() == typeof(string))
                {
                    CurrentVisiblePage = (string)parameter;
                }
                else if (parameter.GetType() == typeof(MainMenuNavigationItemData))
                {
                    navigationData = (MainMenuNavigationItemData)parameter;
                    CurrentVisiblePage = navigationData.Page;
                }
                else
                {
                    throw new InvalidOperationException("Accepts only strings");
                }
            },
            (parameter) => MainNavigationItems.Count > 0
            );
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
