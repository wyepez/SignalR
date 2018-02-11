using Microsoft.AspNet.SignalR.Client;
using System.Threading.Tasks;

namespace Client.Services
{
    public class Hub : IHub
    {
        private HubConnection hubConnection;
        private IChatProxy chatProxy;

        public bool IsConnecting => hubConnection.State == ConnectionState.Connecting;
        public bool IsConnected => hubConnection.State == ConnectionState.Connected;
        public bool IsReconnecting => hubConnection.State == ConnectionState.Reconnecting;
        public bool IsDisconnected => hubConnection.State == ConnectionState.Disconnected;
        public string Url => hubConnection.Url;

        public IChatProxy ChatProxy => chatProxy;

        public Hub()
        {
            hubConnection = new HubConnection("http://127.0.0.1:8082");
            chatProxy = new ChatProxy(hubConnection.CreateHubProxy("ChatHub"));
        }

        public Task Start()
        {
            return hubConnection.Start();
        }

        public void Stop()
        {
            hubConnection.Stop();
        }
    }
}
