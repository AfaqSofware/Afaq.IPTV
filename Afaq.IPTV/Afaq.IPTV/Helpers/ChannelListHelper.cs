using System.Collections.ObjectModel;
using System.Linq;
using Afaq.IPTV.Models;

namespace Afaq.IPTV.Helpers
{
    public class ChannelListHelper
    {
        public ObservableCollection<Channel> GetChannels(string key, ObservableCollection<Channel> source)
        {
            if (string.IsNullOrEmpty(key)) {
                return source;
            }
            return
                new ObservableCollection<Channel>(source.Where(ch => ch.Name.ToUpper().Contains(key.ToUpper())));
        }
    }
}