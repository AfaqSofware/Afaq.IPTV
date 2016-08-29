using System.Linq;

namespace Afaq.IPTV.Models
{
    public static class ExtensionMethods
    {
        public static ChannelList Merge(this ChannelList firstChannelList, ChannelList secondChannelList)
        {
            var result = firstChannelList;
            var ids = (from c in firstChannelList.Channels
                select c.Id).Distinct();

            foreach (var channel in secondChannelList.Channels)
            {
                if (ids.Contains(channel.Id))
                    continue;
                result.Channels.Add(channel);
            }
            return result;
        }
    }
}