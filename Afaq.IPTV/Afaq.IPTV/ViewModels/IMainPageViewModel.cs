using System.Collections.Generic;
using System.Windows.Input;
using Afaq.IPTV.Models;
using Prism.Events;
using Prism.Navigation;

namespace Afaq.IPTV.ViewModels
{
    public interface IMainPageViewModel
    {
        List<ChannelList> ChannelLists { get; set; }
     //   Channel CurrentChannel { get; set; }
        ChannelList CurrentChannelList { get; set; }

        void OnNavigatedFrom(NavigationParameters parameters);
        void OnNavigatedTo(NavigationParameters parameters);

        ICommand GetNextChannelListCommand { get;  }
        ICommand GetPreviousChannelListCommand { get; }
        ICommand PlayCurrentChannelCommand { get;  }
        bool CanPlay { get; set; }

        void SetCinemaMode(bool isCinemaMode);
        void SignOut();
        void MoveSelectionUp();
        void MoveSelectionDown();
    }
}