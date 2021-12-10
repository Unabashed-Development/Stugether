using System.Windows.Controls;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for ProfilePage.xaml
    /// </summary>
    public partial class ProfilePage : Page
    {
        /// <summary>
        /// The default constructor for ProfilePageViewModel.
        /// </summary>
        public ProfilePage()
        {
            DataContext = new ProfilePageViewModel();
            InitializeComponent();
        }

        /// <summary>
        /// Initialize another DataContext for ProfilePageViewModel.
        /// </summary>
        /// <param name="viewModel">The viewmodel to be set as DataContext.</param>
        public ProfilePage(ProfilePageViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
