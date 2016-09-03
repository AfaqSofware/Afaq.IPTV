using System.Threading.Tasks;
using Afaq.IPTV.Events;
using Afaq.IPTV.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;

namespace Afaq.IPTV.ViewModels
{
    public class VideoPageViewModel : BindableBase, INavigationAware, IVideoPageViewModel
    {
        private ChannelList _currentChannelList;
        private bool _isChannelTitleVisible;


        public VideoPageViewModel(IEventAggregator eventAggregator)
        {
            Aggregator = eventAggregator;
        }

        public bool IsChannelTitleVisible
        {
            get { return _isChannelTitleVisible; }
            set { SetProperty(ref _isChannelTitleVisible, value); }
        }

        public IEventAggregator Aggregator { get; }

        public ChannelList CurrentChannelList
        {
            get { return _currentChannelList; }
            set { SetProperty(ref _currentChannelList, value); }
        }


        public void OnNavigatedFrom(NavigationParameters parameters)
        {
        }

        public async void OnNavigatedTo(NavigationParameters parameters)
        {
            Aggregator.GetEvent<CinemaModeEvent>().Publish(true); //Turn on Cinema mode 

            if (!parameters.ContainsKey("channelList")) return;
            CurrentChannelList = (ChannelList) parameters["channelList"];

            // Here we do not want to show the channelsTitle in case it is a mobile 
            if (parameters.ContainsKey("isMobile"))
            {
                if ((bool)parameters["isMobile"])
                {
                    IsChannelTitleVisible = false; 
                }
                else {
                    await BlinkChannelTitle();
                }
            }
            else
            {
                await BlinkChannelTitle();
            }

        }


        public async void MoveSelectionUp()
        {
           _currentChannelList.MoveSelectionUp(null);
            await BlinkChannelTitle();
        }

        public async void MoveSelectionDown()
        {
            _currentChannelList.MoveSelectionDown(null);
            await BlinkChannelTitle();
        }


        private async Task BlinkChannelTitle()
        {
            IsChannelTitleVisible = true;
            await Task.Delay(2000);
            IsChannelTitleVisible = false;
        }
    }
  
}