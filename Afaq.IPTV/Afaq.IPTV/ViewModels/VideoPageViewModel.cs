﻿using System;
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
        private readonly IEventAggregator _eventAggregator;
        private string _videoSource;

        public VideoPageViewModel(IEventAggregator eventAggregator)
        {
            _eventAggregator = eventAggregator;

            MessagingCenter.Subscribe<MainPagePhoneViewModel, Channel>(this, "ChannelChanged", OnChannelChanged);
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
            if (Device.Idiom == TargetIdiom.Phone) {
                _eventAggregator.GetEvent<FullScreenEvent>().Publish(new FullScreenEventArgs { IsFullScreen = true, IsPhone = true });
            } else {
                _eventAggregator.GetEvent<FullScreenEvent>().Publish(new FullScreenEventArgs { IsFullScreen = true, IsPhone = false });
            }

            if (parameters.ContainsKey("channel")) {
                var sourceUrl = ((Channel)parameters["channel"]).CurrentSource.VideoSource;
                VideoSource = ((Channel)parameters["channel"]).CurrentSource.VideoSource.ToString();
            }
        }



        private void OnChannelChanged(MainPagePhoneViewModel arg1, Channel newChannel)
        {
            VideoSource = newChannel.CurrentSource.VideoSource.ToString();
        }
    }
}
