using Microsoft.AspNet.SignalR.Client;
using System;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IChatService
    {
        HubConnection HubConnection { get; set; }
        Task JoinGroup(string groupName);
        Task LeaveGroup(string groupName);
        Task SendToGroup(string groupName, string userName, string message, DateTime dateTime);
        IDisposable RegisterReceiveMessageCallback(Action<string, string, DateTime> callback);
    }
}
