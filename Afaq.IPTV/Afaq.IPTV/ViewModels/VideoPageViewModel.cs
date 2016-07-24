using System;
using Afaq.IPTV.Events;
using Afaq.IPTV.Models;
using Prism.Events;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV.ViewModels
{
    public class VideoPageViewModel : BindableBase, INavigationAware, IVideoPageViewModel
    {
        public IEventAggregator Aggregator { get; }
        private string _videoSource;

        public VideoPageViewModel(IEventAggregator eventAggregator)
        {
            Aggregator = eventAggregator;

            MessagingCenter.Subscribe<MainPageViewModel, Channel>(this, "ChannelChanged", OnChannelChanged);
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

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            Aggregator.GetEvent<CinemaModeEvent>().Publish(true); //Turn on Cinema mode 
           

            if (parameters.ContainsKey("channel")) {
                VideoSource = ((Channel)parameters["channel"]).CurrentSource.VideoSource.ToString();
            }
        }



        private void OnChannelChanged(MainPageViewModel arg1, Channel newChannel)
        {
            VideoSource = newChannel.CurrentSource.VideoSource.ToString();
        }
    }
}
