using System;
using System.Collections.ObjectModel;
using System.Linq;
using Afaq.IPTV.Events;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware, IMainPagePhoneViewModel
    {
        private readonly IChannelService _channelService;
        private readonly IEventAggregator _eventAggregator;
        private readonly INavigationService _navigationService;
        private ObservableCollection<ChannelList> _channelLists;


        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            IChannelService channelService)
        {
            _navigationService = navigationService;
            _eventAggregator = eventAggregator;
            _channelService = channelService;
        //    _eventAggregator.GetEvent<BackButtonPressed>().Unsubscribe(OnBackButtonPressed);
            _eventAggregator.GetEvent<BackButtonPressed>().Subscribe(OnBackButtonPressed);
            MessagingCenter.Subscribe<object>(this, "MoveUp", OnMoveUp);
            MessagingCenter.Subscribe<object>(this, "MoveDown", OnMoveDown);
            MessagingCenter.Subscribe<object>(this, "Enter", OnEnter);
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

        public string CurrentListName { get; set; }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
           
        }

        public void SetScreenOrientation()
        {
            if (Device.Idiom == TargetIdiom.Phone) {
                _eventAggregator.GetEvent<FullScreenEvent>()
                    .Publish(new FullScreenEventArgs { IsFullScreen = false, IsPhone = true });
            } else {
                _eventAggregator.GetEvent<FullScreenEvent>()
                    .Publish(new FullScreenEventArgs { IsFullScreen = false, IsPhone = false });
            }

        }
        public async void OnNavigatedTo(NavigationParameters parameters)
        {
           
            if (!parameters.ContainsKey("channels")) return;

            var data = (string) parameters["channels"];
            var result = await _channelService.GetAllChannelsAsync(data);
            ChannelLists = new ObservableCollection<ChannelList>(result);
            // subscribing to the event form all the channelLists                                       
            foreach (var channelList in ChannelLists)
            {
                channelList.ChannelChanged += OnChannelChanged;
            }
        }


        private void OnChannelChanged(object sender, Channel channel)
        {
            var channelParameters = new NavigationParameters();
            if (channel == null) return;
            channelParameters.Add("channel", channel);
            _navigationService.NavigateAsync("VideoPage", channelParameters);
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


        private void OnEnter(object arg1)
        {
            // Need to see which is the acive tab to select the channel from 
        }

        private void OnMoveDown(object arg1)
        {
            // Need to see which is the acive tab to move the selection
        }

        private void OnMoveUp(object arg1)
        {
            // Need to see which is the acive tab to move the selection 
        }
    }
}