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
        private bool _isLogoVisible;


        public VideoPageViewModel(IEventAggregator eventAggregator)
        {
            Aggregator = eventAggregator;
        }

        public bool IsLogoVisible
        {
            get { return _isLogoVisible; }
            set { SetProperty(ref _isLogoVisible, value); }
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
            if (parameters.ContainsKey("isMobile"))
            {
                if ((bool)parameters["isMobile"])
                {
                    IsLogoVisible = false; 
                }
                else
                {
                    IsLogoVisible = true; 
                }
            }
            IsLogoVisible = true;
            await Task.Delay(2000);
            IsLogoVisible = false;
        }

        public async void MoveSelectionUp()
        {
           _currentChannelList.MoveSelectionUp(null);
            IsLogoVisible = true;
            await Task.Delay(2000);
            IsLogoVisible = false;
        }

        public async void MoveSelectionDown()
        {
            _currentChannelList.MoveSelectionDown(null);
            IsLogoVisible = true;
            await Task.Delay(2000);
            IsLogoVisible = false;
        }
    }
  
}