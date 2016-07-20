using System.Collections.ObjectModel;
using Afaq.IPTV.Models;
using Prism.Events;
using Prism.Navigation;

namespace Afaq.IPTV.ViewModels
{
    public interface _IMainPageViewModel
    {
        ObservableCollection<ChannelList> ChannelLists { get; set; }
        string CurrentListName { get; set; }
        INavigationService NavigationService { get; }
        IEventAggregator Aggregator { get; }

        void OnNavigatedFrom(NavigationParameters parameters);
        void OnNavigatedTo(NavigationParameters parameters);
    }
}