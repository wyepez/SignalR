using Client.Services;
using Microsoft.AspNet.SignalR.Client;
using Unity;
using Unity.Injection;
using Unity.Lifetime;

namespace Client.ViewModels
{
    public class UnityLocator
    {
        private readonly UnityContainer unityContainer = new UnityContainer();

        public MainPageViewModel MainPageViewModel => unityContainer.Resolve<MainPageViewModel>();
        public ChatRoomPageViewModel ChatRoomPageViewModel => unityContainer.Resolve<ChatRoomPageViewModel>();

        public UnityLocator()
        {
            var hubConnection = new HubConnection("http://127.0.0.1:8082");
            var chatHubProxy = hubConnection.CreateHubProxy("ChatHub");
            unityContainer.RegisterInstance(hubConnection, new ContainerControlledLifetimeManager());
            unityContainer.RegisterType<IChatService, ChatService>(new InjectionConstructor(hubConnection, chatHubProxy));
        }
    }
}
