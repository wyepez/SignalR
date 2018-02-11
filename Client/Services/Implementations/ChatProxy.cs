using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Client.Services
{
    public class ChatProxy : IChatProxy
    {
        private IHubProxy hubProxy;

        public ChatProxy(IHubProxy hubProxy)
        {
            this.hubProxy = hubProxy;
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
