using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ChatService : IChatService
    {
        private IHubProxy hubProxy;

        public HubConnection HubConnection { get; set; }

        public ChatService(HubConnection hubConnection, IHubProxy hubProxy)
        {
            this.hubProxy = hubProxy;
            HubConnection = hubConnection;
        }

        public Task JoinGroup(string groupName)
        {
            return hubProxy.Invoke("JoinGroup", groupName);
        }

        public Task LeaveGroup(string groupName)
        {
            return hubProxy.Invoke("LeaveGroup", groupName);
        }

        public Task SendToGroup(string groupName, string userName, string message, DateTime dateTime)
        {
            return hubProxy.Invoke("SendToGroup", groupName, userName, message, DateTime.Now);
        }

        public IDisposable RegisterReceiveMessageCallback(Action<string, string, DateTime> callback)
        {
            return hubProxy.On("receivemessage", callback);
        }
    }
}
