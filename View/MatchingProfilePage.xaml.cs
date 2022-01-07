using System.Windows.Controls;
using ViewModel;

namespace View
{
    /// <summary>
    /// Interaction logic for MatchingProfilePage.xaml
    /// </summary>
    public partial class MatchingProfilePage : Page
    {
        public MatchingProfilePage()
        {
            DataContext = new MatchingProfilePageViewModel();
            InitializeComponent();
        }

        public MatchingProfilePage(MatchingProfilePageViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
