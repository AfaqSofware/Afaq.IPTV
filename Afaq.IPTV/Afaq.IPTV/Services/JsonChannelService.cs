using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Afaq.IPTV.Models;
using Newtonsoft.Json;

namespace Afaq.IPTV.Services
{
    public class JsonChannelService : IChannelService
    {
        private ObservableCollection<ChannelList> _channelLists;

        public JsonChannelService()
        {
            _channelLists = new ObservableCollection<ChannelList>();
        }

        public Task<IEnumerable<ChannelList>> GetAllChannelsAsync(string data)
        {
            return Task<IEnumerable<ChannelList>>.Factory.StartNew(() => GetAllChannelsList(data));
        }

        public Task<IEnumerable<Channel>> GetChannelsAsync(string key, string listName)
        {
            return Task<IEnumerable<Channel>>.Factory.StartNew(() => GetChannels(key, listName));
        }

        private IEnumerable<ChannelList> GetAllChannelsList(string data)
        {
            _channelLists = JsonConvert.DeserializeObject<ObservableCollection<ChannelList>>(data);
            return _channelLists;
        }

        private IEnumerable<Channel> GetChannels(string key, string listName)
        {
            if (string.IsNullOrEmpty(listName)) {
                return null;
            }
            if (string.IsNullOrEmpty(key)) {
                foreach (var channelList in _channelLists) {
                    if (channelList.Name == listName) {
                        return channelList.Channels;
                    }
                }
            }
            IEnumerable<Channel> result = null;
            foreach (var channelList in _channelLists) {
                if (channelList.Name == listName) {
                    result = channelList.Channels.Where(ch => ch.Name.ToUpper().Contains(key.ToUpper()));
                }
            }
            return result;
        }
    }
}