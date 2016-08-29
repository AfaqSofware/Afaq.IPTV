using System.Collections.Generic;
using System.Threading.Tasks;
using Afaq.IPTV.Models;

namespace Afaq.IPTV.Services
{
    public interface IChannelService
    {
        /// <summary>
        /// Get All ChannelLists from provided data
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        Task<IEnumerable<ChannelList>> GetAllChannelsAsync(string dataList);

        Task<IEnumerable<ChannelList>> GetAllChannelsAsync(List<string> dataList);
        Task<IEnumerable<Channel>> GetChannelsAsync(string key, string listName);
    }
}