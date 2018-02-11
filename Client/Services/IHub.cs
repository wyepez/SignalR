using System.Threading.Tasks;

namespace Client.Services
{
    public interface IHub
    {
        bool IsConnecting { get; }
        bool IsConnected { get; }
        bool IsReconnecting { get; }
        bool IsDisconnected { get; }
        string Url { get; }

        IChatProxy ChatProxy { get; }

        Task Start();
        void Stop();
    }
}
