using Afaq.IPTV.Models;
using Prism.Navigation;

namespace Afaq.IPTV.ViewModels
{
    public interface IMainPageViewModel
    {
        Channel CurrentChannel { get; set; }
        ChannelList CurrentChannelList { get; set; }
        INavigationService NavigationService { get; }

        void GetNextChannelList();
        void GetPreviousChannelList();
        void OnNavigatedFrom(NavigationParameters parameters);
        void OnNavigatedTo(NavigationParameters parameters);
        void PlayChannel(Channel currentChannel);
    }
}