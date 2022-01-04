using System;
using Gateway;
using System.Collections.ObjectModel;
using System.Windows.Input;
using ViewModel.Commands;
using ViewModel.Mediators;
using System.Linq;
using Model;
using ViewModel.Helpers;

namespace ViewModel
{
    /// <summary>
    /// ViewModel for MainPage
    /// </summary>
    public class MainPageViewModel : ObservableObject
    {
        #region Fields
        private ObservableCollection<MainMenuNavigationItemData> _mainNavigationItems;
        #endregion

        #region Construction
        /// <summary>
        /// Creates a new viewmodel for MainPage
        /// </summary>
        public MainPageViewModel()
        {
            SSHService.Initialize(); // Initialize SSH for the database connection and logging in
            MainNavigationItems = SetObservableCollection(false); // Initialize the default page list
            ViewModelMediators.AuthenticationStateChanged += OnAuthenticationStateChanged;
            ViewModelMediators.MainWindowPageChanged += OnMainWindowPageChanged;
            ViewModelMediators.AuthenticationStateChanged += NotificationHelper.InitializeNotificationThreads;
        }

        /// <summary>
        /// On basis of the authentication state, change the Home page view that needs to be displayed.
        /// </summary>
        private void OnAuthenticationStateChanged() => MainNavigationItems = Account.Authenticated ? SetObservableCollection(true) : SetObservableCollection(false);
        #endregion

        #region Properties
        /// <summary>
        /// Collection of pages for the main navigation menu.
        /// </summary>
        public ObservableCollection<MainMenuNavigationItemData> MainNavigationItems
        {
            get => _mainNavigationItems;
            set
            {
                _mainNavigationItems = value;
                RaisePropertyChanged("MainNavigationItems");
            }
        }

        /// <summary>
        /// Page that is currently visible on the frame.
        /// </summary>
        public string MainWindowPage
        {
            get => ViewModelMediators.MainWindowPage;
            set => ViewModelMediators.MainWindowPage = value;
        }

        /// <summary>
        /// Handles the click events of the main menu buttons.
        /// </summary>
        public ICommand NavigateButtonCommand => new RelayCommand(
            (parameter) =>
            {
                if (parameter.GetType() == typeof(string))
                {
                    MainWindowPage = (string)parameter;
                }
                else if (parameter.GetType() == typeof(MainMenuNavigationItemData))
                {
                    MainMenuNavigationItemData navigationData = (MainMenuNavigationItemData)parameter;
                    MainWindowPage = navigationData.Page;
                }
                else
                {
                    throw new InvalidOperationException("Accepts only strings");
                }
            },
            (parameter) => Account.Authenticated
            );
        #endregion

        #region Methods
        /// <summary>
        /// Initializes a new ObservableCollection of MainMenuNavigationItemData.
        /// </summary>
        /// <param name="logoutPage">Set this to true if you want the Home page to be the logout page. Default is login page.</param>
        /// <returns>ObservableCollection of MainMenuNavigationItemData.</returns>
        private ObservableCollection<MainMenuNavigationItemData> SetObservableCollection(bool logoutPage)
        {
            var collection = new ObservableCollection<MainMenuNavigationItemData>();
            if (logoutPage)
            {
                MainWindowPage = @"HomePages\HomePageAfterLogin.xaml";
                collection.Add(new MainMenuNavigationItemData("Home", MainWindowPage, null));
            }
            else
            {
                MainWindowPage = @"HomePages\HomePageBeforeLogin.xaml";
                collection.Add(new MainMenuNavigationItemData("Home", MainWindowPage, null));
            }
            collection.Add(new MainMenuNavigationItemData("Profiel", "ProfilePage.xaml", null));
            collection.Add(new MainMenuNavigationItemData("Zoeken naar matches", "MatchingProfilePage.xaml", null));
            collection.Add(new MainMenuNavigationItemData("Mijn matches", "OverviewMatches.xaml", null));
            collection.Add(new MainMenuNavigationItemData("Zoekvoorkeuren", "SearchPreferencePage.xaml", null));
            collection.Add(new MainMenuNavigationItemData("Instellingen", "ProfileSettings.xaml", null));

            return collection;
        }

        private void OnMainWindowPageChanged()
        {
            RaisePropertyChanged("MainWindowPage");
        }
        #endregion

        #region MainMenuNavigationItemData
        /// <summary>
        /// The data for a menu item on the main navigation menu.
        /// </summary>
        public struct MainMenuNavigationItemData
        {
            /// <summary>
            /// Creates an item of data for the main navigation menu.
            /// </summary>
            /// <param name="title">The title of the page shown on the top of the main page.</param>
            /// <param name="page">The page to be navigated to.</param>
            /// <param name="extraInformation">Optional extra information to be given when navigating.</param>
            public MainMenuNavigationItemData(string title, string page, object extraInformation)
            {
                Title = title;
                Page = page;
                ExtraInformation = extraInformation;
            }

            /// <summary>
            /// The title of the page shown on the top of the main page.
            /// </summary>
            public string Title { get; set; }
            /// <summary>
            /// The page to be navigated to.
            /// </summary>
            public string Page { get; set; }
            /// <summary>
            /// Optional extra information to be given when navigating.
            /// </summary>
            public object ExtraInformation { get; set; }
        }
        #endregion
    }
}
