using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.Models;

namespace Afaq.IPTV.Services
{
    public class M3UChannelService : IChannelService
    {
        private readonly ObservableCollection<ChannelList> _channelLists;

        public M3UChannelService()
        {
            _channelLists = new ObservableCollection<ChannelList>();
        }

        public Task<IEnumerable<ChannelList>> GetAllChannelsAsync(string data)
        {
            return Task<IEnumerable<ChannelList>>.Factory.StartNew(() => GetAllChannelsList(data));
        }

        public Task<IEnumerable<ChannelList>> GetAllChannelsAsync(List<string> dataList)
        {
            return Task<IEnumerable<ChannelList>>.Factory.StartNew(() => GetAllChannelsList(dataList));
        }

        public Task<IEnumerable<Channel>> GetChannelsAsync(string key, string listName)
        {
            return Task<IEnumerable<Channel>>.Factory.StartNew(() => GetChannels(key, listName));
        }

        private IEnumerable<ChannelList> GetAllChannelsList(List<string> dataList)
        {
            var channelListDict = new Dictionary<string,ChannelList>();
            var resultList = new List<IEnumerable<ChannelList>>();
            foreach (var data in dataList)
            {
                resultList.Add(GetAllChannelsList(data)); 
            }
            foreach (var channelLists in resultList)
            {
                foreach (var channelList in channelLists)
                {
                    if (channelListDict.ContainsKey(channelList.Name))
                    {
                        channelListDict[channelList.Name] = channelListDict[channelList.Name].Merge(channelList);
                    }
                    else
                    {
                        channelListDict[channelList.Name] = channelList; 
                    }
                   
                }
            }
            var result = new ObservableCollection<ChannelList>();
            foreach (var channelList in channelListDict.Values)
            {
                result.Add(channelList); 
            }
            return result;
        }
        private IEnumerable<ChannelList> GetAllChannelsList(string data)
        {
            var allChanelsList = new ChannelList() {Name = "All"};
            var otherChannelsList = new ChannelList() {Name = "Others"};
            var favouriteChannelList = new ChannelList(){ Name = "Favourites" };

            var playlist = new M3UPlaylist(data);

            #region Prepare AllChannelsList

            foreach (var playlistTrack in playlist.M3UTracks)
            {
                var channel = new Channel
                {
                    CurrentSource = new Source {VideoSource = playlistTrack.Location},
                    Name = $"{playlistTrack.Information.Id}. {playlistTrack.Information.Title}" ,
                    Id = playlistTrack.Information.Id,
                    Group = playlistTrack.Information.Group
                };
                if (!string.IsNullOrEmpty(playlistTrack.Information.Logo))
                {
                    channel.Logo = new Uri(playlistTrack.Information.Logo);
                }
                allChanelsList.Channels.Add(channel);
                allChanelsList.FullChannels.Add(channel);
            }

            #endregion

            #region Generating ChannelLists from all Channelslist

            var channelLists = (from channel in allChanelsList.Channels
                orderby channel.Id
                group channel by channel.Group
                into channelGroups
                select channelGroups).ToDictionary(g => g.Key, g => g.ToList());

            #endregion

           // _channelLists.Add(favouriteChannelList);
            _channelLists.Add(allChanelsList);

            foreach (var channelList in channelLists)
            {
                if (string.IsNullOrEmpty(channelList.Key))
                {
                    foreach (var channel in channelList.Value)
                    {
                        otherChannelsList.Channels.Add(channel);
                        otherChannelsList.FullChannels.Add(channel);
                    }
                }
                else
                {
                    _channelLists.Add(new ChannelList()
                    {
                        Name = channelList.Key,
                        Channels = new ObservableCollection<Channel>(channelList.Value),
                        FullChannels = new ObservableCollection<Channel>(channelList.Value)
                    });
                }
            }
            if (otherChannelsList.Channels.Any())
            {
                _channelLists.Add(otherChannelsList);
            }

            return _channelLists;
        }

        private IEnumerable<Channel> GetChannels(string key, string listName)
        {
            if (string.IsNullOrEmpty(listName))
            {
                return null;
            }
            if (string.IsNullOrEmpty(key))
            {
                foreach (var channelList in _channelLists)
                {
                    if (channelList.Name == listName)
                    {
                        return channelList.Channels;
                    }
                }
            }
            IEnumerable<Channel> result = null;
            foreach (var channelList in _channelLists)
            {
                if (channelList.Name == listName)
                {
                    result = channelList.Channels.Where(ch => ch.Name.ToUpper().Contains(key.ToUpper()));
                }
            }
            return result;
        }
    }
}