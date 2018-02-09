using Client.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace Client.Views
{
    public sealed partial class MainPage : Page
    {
        private MainPageViewModel ViewModel => DataContext as MainPageViewModel;

        public MainPage()
        {
            InitializeComponent();
            NavigationCacheMode = NavigationCacheMode.Enabled;
        }
    }
}