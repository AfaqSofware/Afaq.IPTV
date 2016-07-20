using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Afaq.IPTV.Events;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;


namespace Afaq.IPTV.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware, IMainPageViewModel
    {
        private ChannelList _currentChannelList;
        private readonly IChannelService _channelService;
        private List<ChannelList> _channelLists;
        private Channel _currentChannel;

        public MainPageViewModel(IChannelService channelService, INavigationService navigationService)
        {
            _channelService = channelService;
            NavigationService = navigationService;
        }

        public ChannelList CurrentChannelList
        {
            get { return _currentChannelList; }
            set
            {
                _currentChannelList = value;
                OnPropertyChanged();
            }
        }

        public Channel CurrentChannel
        {
            get { return _currentChannel; }
            set
            {
                _currentChannel = value;
                OnPropertyChanged();
            }
        }

        public void GetNextChannelList()
        {
            if (_channelLists.Count < 1)
            {
                return;
            }
            var currentChannelListIndex = _channelLists.IndexOf(_currentChannelList);
            if (_channelLists.Last() == _currentChannelList)
            {
                CurrentChannelList = _channelLists.First();
            }
            else
            {
                CurrentChannelList = _channelLists[currentChannelListIndex + 1];
            }
        }

        public INavigationService NavigationService { get; }

        public void GetPreviousChannelList()
        {
            if (_channelLists.Count < 1)
            {
                return;
            }
            var currentChannelListIndex = _channelLists.IndexOf(_currentChannelList);
            if (_channelLists.First() == _currentChannelList)
            {
                CurrentChannelList = _channelLists.Last();
            }
            else
            {
                CurrentChannelList = _channelLists[currentChannelListIndex - 1];
            }
        }

        public void PlayChannel(Channel currentChannel)
        {
            if (currentChannel == null) return;
            var channelParameters = new NavigationParameters {{"channel", currentChannel}};
            NavigationService.NavigateAsync("VideoPage", channelParameters);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("channels")) return;

            var data = (string) parameters["channels"];
            var result = await _channelService.GetAllChannelsAsync(data);
            _channelLists = new List<ChannelList>(result);
        }
    }
}


//public class MainPageViewModel : BindableBase, INavigationAware, IMainPageViewModel
//{
//    //private readonly IChannelService _channelService;

//    //private ObservableCollection<ChannelList> _channelLists;


//    //public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
//    //    IChannelService channelService)
//    //{
//    //    NavigationService = navigationService;
//    //    Aggregator = eventAggregator;
//    //    _channelService = channelService;
//    //}

//    //public INavigationService NavigationService { get; }
//    //public IEventAggregator Aggregator { get; }

//    //public ObservableCollection<ChannelList> ChannelLists
//    //{
//    //    get { return _channelLists; }
//    //    set
//    //    {
//    //        if (Equals(value, _channelLists)) return;
//    //        _channelLists = value;
//    //        OnPropertyChanged();
//    //    }
//    //}

//    //public string CurrentListName { get; set; }

//    //public void OnNavigatedFrom(NavigationParameters parameters)
//    //{

//    //}


//    //public async void OnNavigatedTo(NavigationParameters parameters)
//    //{

//    //    if (!parameters.ContainsKey("channels")) return;

//    //    var data = (string) parameters["channels"];
//    //    var result = await _channelService.GetAllChannelsAsync(data);
//    //    ChannelLists = new ObservableCollection<ChannelList>(result);

//    //    // subscribing to the event form all the channelLists                                       
//    //    foreach (var channelList in ChannelLists)
//    //    {
//    //        channelList.ChannelChanged += OnChannelChanged;
//    //    }
//    //}


//    //private void OnChannelChanged(object sender, Channel channel)
//    //{
//    //    var channelParameters = new NavigationParameters();
//    //    if (channel == null) return;
//    //    channelParameters.Add("channel", channel);
//    //    NavigationService.NavigateAsync("VideoPage", channelParameters);
//    //}


//}