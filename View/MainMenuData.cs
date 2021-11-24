using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace View
{
    public class MainMenuData
    {
        /// <summary>
        /// Collection of pages for the main navigation menu
        /// </summary>
        public ObservableCollection<MainMenuNavigationItemData> MainNavigationItems { get; } = new ObservableCollection<MainMenuNavigationItemData>()
        {
            // Example:
            //new MainMenuNavigationItemData("Test", new TestPage(), "TestData"),
            //new MainMenuNavigationItemData("Test2", new TestPage2(), null)
            new MainMenuNavigationItemData("Profile", new ProfilePage(), null)
        };

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
            public MainMenuNavigationItemData(string title, UIElement page, object extraInformation)
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
            public UIElement Page { get; set; }
            /// <summary>
            /// Optional extra information to be given when navigating
            /// </summary>
            public object ExtraInformation { get; set; }
        }
    }
}
