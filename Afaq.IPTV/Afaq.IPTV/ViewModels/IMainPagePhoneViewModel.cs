using System.Collections.ObjectModel;
using Afaq.IPTV.Models;
using Prism.Navigation;

namespace Afaq.IPTV.ViewModels
{
    public interface IMainPagePhoneViewModel
    {
        ObservableCollection<ChannelList> ChannelLists { get; set; }
        string CurrentListName { get; set; }

        void OnNavigatedFrom(NavigationParameters parameters);
        void OnNavigatedTo(NavigationParameters parameters);
    }
}