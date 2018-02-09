using Client.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Client.Views
{
    public sealed partial class ChatRoomPage : Page
    {
        public ChatRoomPageViewModel ViewModel => DataContext as ChatRoomPageViewModel;

        public ChatRoomPage()
        {
            InitializeComponent();
        }
    }
}
