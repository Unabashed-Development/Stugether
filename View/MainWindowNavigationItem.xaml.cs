using System.Windows;
using System.Windows.Controls;

namespace View
{
    /// <summary>
    /// A menu item for the main navigation menu
    /// </summary>
    public partial class MainWindowNavigationItem : RadioButton
    {
        public static readonly DependencyProperty PageProperty = DependencyProperty.Register(
            "Page", typeof(string), typeof(MainWindowNavigationItem));
        /// <summary>
        /// The page to be navigated to
        /// </summary>
        public string Page
        {
            get { return GetValue(PageProperty) as string; }
            set { SetValue(PageProperty, value); }
        }

        public static readonly DependencyProperty ExtraNavigationInformationProperty = DependencyProperty.Register(
            "ExtraNavigationInformation", typeof(object), typeof(MainWindowNavigationItem));
        /// <summary>
        /// Optional extra information to be given when navigating to the page
        /// </summary>
        public object ExtraNavigationInformation
        {
            get { return GetValue(ExtraNavigationInformationProperty) as object; }
            set { SetValue(ExtraNavigationInformationProperty, value); }
        }

        public MainWindowNavigationItem()
        {
            InitializeComponent();
        }


    }
}
