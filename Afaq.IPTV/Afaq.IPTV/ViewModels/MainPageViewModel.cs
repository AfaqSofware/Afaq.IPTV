using System;
using System.Collections.ObjectModel;
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
        private readonly IChannelService _channelService;
        private readonly IEventAggregator _eventAggregator;

        private ObservableCollection<ChannelList> _channelLists;


        public MainPageViewModel(INavigationService navigationService, IEventAggregator eventAggregator,
            IChannelService channelService)
        {
            NavigationService = navigationService;
            _eventAggregator = eventAggregator;
            _channelService = channelService;
            _eventAggregator.GetEvent<BackButtonPressed>().Subscribe(OnBackButtonPressed);
        }

        public INavigationService NavigationService { get; }

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
            NavigationService.NavigateAsync("VideoPage", channelParameters);
        }

        private async void OnBackButtonPressed(object obj)
        {
            try {
                await NavigationService.GoBackAsync();
            }
            catch (Exception) {
                throw;
            }
        }



    }
}