using Client.Services;
using Client.Views;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Common;
using Template10.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Client.ViewModels
{
    public class ChatRoomPageViewModel : ViewModelBase
    {
        private IChatService chatService;

        private string groupName;
        public string GroupName
        {
            get => groupName;
            set => Set(ref groupName, value);
        }

        private string userName;
        public string UserName
        {
            get => userName;
            set => Set(ref userName, value);
        }

        private string message;
        public string Message
        {
            get => message;
            set => Set(ref message, value);
        }

        private string error;
        public string Error
        {
            get => error;
            set => Set(ref error, value);
        }

        private IDisposable ReceiveMessageHandler { get; set; }
        public ObservableCollection<string> Messages { get; set; }

        public ChatRoomPageViewModel(IChatService chatService)
        {
            this.chatService = chatService;
            Messages = new ObservableCollection<string>();
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            dynamic info = parameter;
            GroupName = info.GroupName;
            UserName = info.UserName;
            App myApp = (Application.Current as App);
            ReceiveMessageHandler = chatService.RegisterReceiveMessageCallback(ReceiveMessage);
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        private void ReceiveMessage(string userName, string message, DateTime sendTime)
        {
            WindowWrapper.Current().Dispatcher.Dispatch(() => { Messages.Add($"{sendTime.ToString("MM-dd HH:mm:ss")}\n{userName}: {message}"); });
        }

        private DelegateCommand sendCommand;
        public DelegateCommand SendCommand => sendCommand ?? (sendCommand = new DelegateCommand(() =>
        {
            Error = string.Empty;
            var myApp = (Application.Current as App);
            if (chatService.HubConnection.State != ConnectionState.Connected)
            {
                Error = "Disconnected!";
                return;
            }

            string message = Message.Trim();
            if (message.Length > 0)
            {
                chatService.SendToGroup(GroupName, UserName, message, DateTime.Now);
            }
            Message = string.Empty;
        }));

        private DelegateCommand exitCommand;
        public DelegateCommand ExitCommand => exitCommand ?? (exitCommand = new DelegateCommand(async () =>
        {
            App myApp = (Application.Current as App);
            if (chatService.HubConnection.State == ConnectionState.Connected)
            {
                await chatService.LeaveGroup(groupName);
                ReceiveMessageHandler.Dispose();
            }
            NavigationService.Navigate(typeof(MainPage), new { GroupName, UserName });
        }));

        private DelegateCommand<string> footerCommand;
        public DelegateCommand<string> FooterCommand => footerCommand ?? (footerCommand = new DelegateCommand<string>(async (link) =>
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(link));
        }));
    }
}
