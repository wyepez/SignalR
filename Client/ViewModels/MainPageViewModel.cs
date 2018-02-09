using Client.Services;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Mvvm;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Navigation;

namespace Client.ViewModels
{
    public class MainPageViewModel : ViewModelBase
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

        private string error;
        public string Error
        {
            get => error;
            set => Set(ref error, value);
        }

        public MainPageViewModel(IChatService chatService)
        {
            this.chatService = chatService;
        }

        public override Task OnNavigatedToAsync(object parameter, NavigationMode mode, IDictionary<string, object> state)
        {
            if (parameter != null)
            {
                dynamic p = parameter;
                GroupName = p.GroupName;
                UserName = p.UserName;
            }
            return base.OnNavigatedToAsync(parameter, mode, state);
        }

        private bool isEnabled = true;
        private DelegateCommand joinCommand;
        public DelegateCommand JoinCommand => joinCommand ?? (joinCommand = new DelegateCommand(async () =>
        {
            isEnabled = false; JoinCommand.RaiseCanExecuteChanged();
            Error = string.Empty;
            string groupName = GroupName.Trim();
            string userName = UserName.Trim();
            if (groupName.Length == 0 || userName.Length == 0)
            {
                Error = "Group & user name can't be empty.";
                isEnabled = true; JoinCommand.RaiseCanExecuteChanged();
                return;
            }
            //Connect to hub 
            App myApp = (Application.Current as App);
            if (chatService.HubConnection.State != ConnectionState.Connected)
            {
                try
                {
                    await chatService.HubConnection.Start();
                }
                catch
                {
                    Error = $"Can't connect to server {chatService.HubConnection.Url}";
                    isEnabled = true; JoinCommand.RaiseCanExecuteChanged();
                    return;
                }
            }
            //join to group
            if (chatService.HubConnection.State == ConnectionState.Connected)
            {
                await chatService.JoinGroup(groupName);
                NavigationService.Navigate(typeof(Views.ChatRoomPage), new { GroupName = groupName, UserName = userName });
            }
            else
            {
                Error = $"Can't connect to server {chatService.HubConnection.Url}";
            }
            isEnabled = true; JoinCommand.RaiseCanExecuteChanged();
        }, () => isEnabled));

        private DelegateCommand<string> footerCommand;
        public DelegateCommand<string> FooterCommand => footerCommand ?? (footerCommand = new DelegateCommand<string>(async (link) =>
        {
            await Windows.System.Launcher.LaunchUriAsync(new Uri(link));
        }));
    }
}
