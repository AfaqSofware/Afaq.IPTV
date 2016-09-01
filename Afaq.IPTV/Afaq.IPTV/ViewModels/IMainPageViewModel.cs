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
        ChannelList CurrentChannelList { get; set; }
        ICommand GetNextChannelListCommand { get;  }
        ICommand GetPreviousChannelListCommand { get; }
        ICommand PlayCurrentChannelCommand { get;  }
        bool CanPlay { get; set; }
        bool IsMobile { get; set; }
        void SetCinemaMode(bool isCinemaMode);
        void SignOut();
        void MoveSelectionUp();
        void MoveSelectionDown();
    }
}