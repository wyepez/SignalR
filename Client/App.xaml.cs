using Client.ViewModels;
using Client.Views;
using Microsoft.AspNet.SignalR.Client;
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
        #region SignalR
        public HubConnection HubConnection { get; set; }
        public IHubProxy HubProxy { get; set; }

        private void SignalR()
        {
            HubConnection = new HubConnection("http://127.0.0.1:8082");
            HubProxy = HubConnection.CreateHubProxy("ChatHub");
        }
        #endregion

        public App()
        {
            InitializeComponent();
            SplashFactory = (e) => new Splash(e);

            #region app settings

            ShowShellBackButton = false;
            SignalR();

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
