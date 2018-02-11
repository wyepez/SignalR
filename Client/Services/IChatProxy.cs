using System;
using System.Threading.Tasks;

namespace Client.Services
{
    public interface IChatProxy
    {
        Task JoinGroup(string groupName);
        Task LeaveGroup(string groupName);
        Task SendToGroup(string groupName, string userName, string message, DateTime dateTime);
        IDisposable RegisterReceiveMessageCallback(Action<string, string, DateTime> callback);
    }
}
