using System.Collections.Generic;
using System.Threading.Tasks;
using Afaq.IPTV.Models;

namespace Afaq.IPTV.Services
{
    public interface IChannelService
    {
        Task<IEnumerable<ChannelList>> GetAllChannelsAsync(string data);
        Task<IEnumerable<Channel>> GetChannelsAsync(string key, string listName);
    }
}