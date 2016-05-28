using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afaq.IPTV.Events;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV.ViewModels
{
    public class MainPageTablet2ViewModel : BindableBase, INavigationAware
    {
        private readonly IChannelService _channelService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private ObservableCollection<ChannelList> _channelLists;
        private ObservableCollection<Channel> _channels;
        private Channel _currentChannel;
        private string _searchKey;
        private string _videoSource;
        private bool _isPanelVisbile;


        public MainPageTablet2ViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            IChannelService channelService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _channelService = channelService;
            _eventAggregator.GetEvent<BackButtonPressed>().Subscribe(OnBackButtonPressed);
            MessagingCenter.Subscribe<object>(this, "MoveUp", OnMoveUp);
            MessagingCenter.Subscribe<object>(this, "MoveDown", OnMoveDown);
            MessagingCenter.Subscribe<object>(this, "Enter", OnEnter);
        }

        public string VideoSource
        {
            get { return _videoSource; }
            set
            {
                if (Equals(value, _videoSource)) return;
                _videoSource = value;
                OnPropertyChanged();
            }
        }

        public string SearchKey
        {
            get { return _searchKey; }
            set
            {
                if (value == _searchKey) return;
                _searchKey = value;
                UpdateChannels(value);
                OnPropertyChanged();
            }
        }

        public bool IsPanelVisbile
        {
            get { return _isPanelVisbile; }
            set
            {
                _isPanelVisbile = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<ChannelList> ChannelLists
        {
            get { return _channelLists; }
            set
            {
                if (Equals(value, _channelLists)) return;
                _channelLists = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Channel> Channels
        {
            get { return _channels; }
            set
            {
                _channels = value;

                OnPropertyChanged();
            }
        }

        public Channel CurrentChannel
        {
            get { return _currentChannel; }
            set
            {
                if (value == null) {
                    return;
                }
                _currentChannel = value;
                VideoSource = value.CurrentSource.VideoSource.OriginalString;
                OnPropertyChanged();
          
                //var channelParameters = new NavigationParameters { { "channel", value } };
                //_navigationService.NavigateAsync("VideoPage", channelParameters);
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("channels")) return;

            var data = (string)parameters["channels"];
            var result = await _channelService.GetAllChannelsAsync(data);
            ChannelLists = new ObservableCollection<ChannelList>(result);
            Channels = ChannelLists[0].Channels;

            if (Device.Idiom == TargetIdiom.Phone) {
                _eventAggregator.GetEvent<FullScreenEvent>()
                    .Publish(new FullScreenEventArgs { IsFullScreen = false, IsPhone = true });
            } else {
                _eventAggregator.GetEvent<FullScreenEvent>()
                    .Publish(new FullScreenEventArgs { IsFullScreen = false, IsPhone = false });
            }
        }

        private async void OnBackButtonPressed(object obj)
        {
            try {
                await _navigationService.GoBackAsync();
            }
            catch (Exception) {
                throw;
            }
        }

        private async void UpdateChannels(string key)
        {
            var result = await _channelService.GetChannelsAsync(key, "All");
            Channels = new ObservableCollection<Channel>(result);
        }


        private void OnEnter(object arg1)
        {
            IsPanelVisbile = !IsPanelVisbile;
        }

        private void OnMoveDown(object arg1)
        {
            IsPanelVisbile = true;
            var index = Channels.IndexOf(CurrentChannel);
            if (index == -1) {
                if (Channels.Any()) {
                    CurrentChannel = Channels.First();
                }
            }
            if (index < Channels.Count - 1) {
                CurrentChannel = Channels[index + 1];
            }
        }

        private void OnMoveUp(object arg1)
        {
            IsPanelVisbile = true;
            var index = Channels.IndexOf(CurrentChannel);
            if (index == -1) {
                if (Channels.Any()) {
                    CurrentChannel = Channels.First();
                }
            }
            if (index > 0) {
                CurrentChannel = Channels[index - 1];
            }
        }
    }
}
