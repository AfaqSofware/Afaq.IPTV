using System.Collections.ObjectModel;

namespace Afaq.IPTV.Models
{
    public class ChannelList
    {
        public string Name { get; set; }
        public ObservableCollection<Channel> Channels { get; set; }

        public ChannelList()
        {
            Channels = new ObservableCollection<Channel>();
            
        }
    }
}
