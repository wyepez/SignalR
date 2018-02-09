using Client.ViewModels;
using Client.Views;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Services.NavigationService;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Data;

namespace Client
{
    [Bindable]
    sealed partial class App : BootStrapper
    {
        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Splash(e);

            #region app settings

            ShowShellBackButton = false;

            #endregion
        }

        public override async Task OnStartAsync(StartKind startKind, IActivatedEventArgs args)
        {
            // TODO: add your long-running task here
            await NavigationService.NavigateAsync(typeof(MainPage));
        }

        public override INavigable ResolveForPage(Page page, NavigationService navigationService)
        {
            var locator = Resources["Locator"] as UnityLocator;
            switch (page)
            {
                case MainPage p:
                    return locator.MainPageViewModel;
                case ChatRoomPage p:
                    return locator.ChatRoomPageViewModel;
                default:
                    return base.ResolveForPage(page, navigationService);
            }
        }
    }
}
