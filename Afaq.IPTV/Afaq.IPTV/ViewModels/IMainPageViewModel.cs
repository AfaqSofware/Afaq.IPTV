using System.Collections.ObjectModel;
using Afaq.IPTV.Models;
using Prism.Navigation;

namespace Afaq.IPTV.ViewModels
{
    public interface IMainPageViewModel
    {
        ObservableCollection<ChannelList> ChannelLists { get; set; }
        string CurrentListName { get; set; }
        INavigationService NavigationService { get; }

        void OnNavigatedFrom(NavigationParameters parameters);
        void OnNavigatedTo(NavigationParameters parameters);
    }
}