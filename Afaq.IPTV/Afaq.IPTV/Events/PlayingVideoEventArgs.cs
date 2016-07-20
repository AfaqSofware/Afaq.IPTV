using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afaq.IPTV.Services;
using Prism.Navigation;

namespace Afaq.IPTV.Events
{
    public class PlayingVideoEventArgs
    {
        public bool IsFullScreen { get; set; }
        public bool IsPhone { get; set; }
    }

    public class GoToMainPageEventArgs
    {
        public IChannelService ChannelService; 
        public INavigationService NavigationService;
        public string ChannelsString; 
    }
}
