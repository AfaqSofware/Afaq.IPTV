using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using Afaq.IPTV.Events;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services;
using Prism.Commands;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;


namespace Afaq.IPTV.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware, IMainPageViewModel
    {
        private ChannelList _currentChannelList;
        private readonly IChannelService _channelService;
        private readonly INavigationService _navigationService;
        private List<ChannelList> _channelLists;
        private string _currentVideoSource;
        private readonly IEventAggregator _eventAggregator;
        private bool _isHardwareDecoding;

        public MainPageViewModel(IChannelService channelService, INavigationService navigationService, IEventAggregator eventAggregator)
        {
            _channelService = channelService;
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            GetNextChannelListCommand = new DelegateCommand(GetNextChannelList);
            GetPreviousChannelListCommand = new DelegateCommand(GetPreviousChannelList);
            PlayCurrentChannelCommand = new DelegateCommand<Channel>(PlayChannel);
            
        }

       

        public List<ChannelList> ChannelLists
        {
            get { return _channelLists; }
            set
            {
                if (Equals(value, _channelLists)) return;
                SetProperty(ref _channelLists, value);
            }
        }

        public ChannelList CurrentChannelList
        {
            get { return _currentChannelList; }
            set
            {
                SetProperty(ref _currentChannelList, value);
            }
        }


        public bool IsHardwareDecoding
        {
            get { return _isHardwareDecoding; }
            set { SetProperty(ref _isHardwareDecoding, value); }
        }

        public string CurrentVideoSource
        {
            get { return _currentVideoSource; }
            set
            {
                SetProperty(ref _currentVideoSource, value);
            }
        }

        public void MoveSelectionDown()
        {
            var index = CurrentChannelList.Channels.IndexOf(CurrentChannelList.CurrentChannel);
            if (index == -1)
            {
                if (CurrentChannelList.Channels.Any())
                {
                    CurrentChannelList.CurrentChannel = CurrentChannelList.Channels.First();
                    return; 
                }
            }
            if (index < CurrentChannelList.Channels.Count - 1)
            {
                CurrentChannelList.CurrentChannel = CurrentChannelList.Channels[index + 1];
            }
        }

        public void MoveSelectionUp()
        {
            var index = CurrentChannelList.Channels.IndexOf(CurrentChannelList.CurrentChannel);
            if (index == -1)
            {
                if (CurrentChannelList.Channels.Any())
                {
                    CurrentChannelList.CurrentChannel = CurrentChannelList.Channels.First();
                    return;
                }
            }
            if (index > 0)
            {
                CurrentChannelList.CurrentChannel = CurrentChannelList.Channels[index - 1];
            }
        }



        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            if (!parameters.ContainsKey("channels")) return;

            var data = (string) parameters["channels"];
            var result = await _channelService.GetAllChannelsAsync(data);
            ChannelLists = new List<ChannelList>(result);
            if (_channelLists.Any())
            {
                CurrentChannelList = _channelLists[0];
            }
    

        }


        #region Commands

        public ICommand GetNextChannelListCommand { get;  }
        public ICommand GetPreviousChannelListCommand { get;  }
        public ICommand PlayCurrentChannelCommand { get; }
        public void SetCinemaMode(bool isCinemaMode)
        {
            _eventAggregator.GetEvent<CinemaModeEvent>().Publish(isCinemaMode);
        }

        public void SignOut()
        {
            _navigationService.NavigateAsync(new Uri("http://www.Afaq.com/LoginPage", UriKind.Absolute), new NavigationParameters { { "IsAutoLogin", false } }, true).Wait();
        }


        private void GetNextChannelList()
        {
            if (_channelLists.Count < 1) {
                return;
            }
            var currentChannelListIndex = _channelLists.IndexOf(_currentChannelList);
            if (_channelLists.Last() == _currentChannelList) {
                CurrentChannelList = _channelLists.First();
            } else {
                CurrentChannelList = _channelLists[currentChannelListIndex + 1];
            }
        }

        private void GetPreviousChannelList()
        {
            if (_channelLists.Count < 1) {
                return;
            }
            var currentChannelListIndex = _channelLists.IndexOf(_currentChannelList);
            if (_channelLists.First() == _currentChannelList) {
                CurrentChannelList = _channelLists.Last();
            } else {
                CurrentChannelList = _channelLists[currentChannelListIndex - 1];
            }
        }

        private void PlayChannel(Channel channelList)
        {
            var navigationParameters = new NavigationParameters { { "channelList", _currentChannelList } };
            _navigationService.NavigateAsync("VideoPage", navigationParameters);

     
        }

        #endregion

        private async void PreviewChannel(string channelPath)
        {
            await Task.Delay(1000);
            if (channelPath == CurrentChannelList.CurrentChannel.CurrentSource.VideoSource)
            {
                System.Diagnostics.Debug.WriteLine("### Playing Channel ###");
                CurrentVideoSource = channelPath;
            }
           
        }

        private void OnChannelChanged(object sender, Channel channel)
        {
            var channelParameters = new NavigationParameters();
            if (channel == null) return;
            channelParameters.Add("channel", channel);
            _navigationService.NavigateAsync("VideoPage", channelParameters);
        }
    }
}