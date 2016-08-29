using System;
using System.ComponentModel;
using Afaq.IPTV.Controls;
using Afaq.IPTV.Droid.CustomRenderers;
using Afaq.IPTV.Droid.Players.VlcPlayer;
using Afaq.IPTV.Helpers;
using Android.Net;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(VlcPlayerView), typeof(VlcPlayerViewRenderer))]

namespace Afaq.IPTV.Droid.CustomRenderers
{
    public class VlcPlayerViewRenderer : ViewRenderer
    {
        
        private VlcVideoPlayer _vlcVideoPlayer;
        private Android.Net.Uri _uri; 
        public VlcPlayerViewRenderer()
        {
            RootView.SystemUiVisibility = StatusBarVisibility.Hidden;            
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
         
            var vlcPlayerView = Element as VlcPlayerView;

            if ((vlcPlayerView == null) || (e.OldElement != null)) return;
            _vlcVideoPlayer = new VlcVideoPlayer(Context);
            
            _vlcVideoPlayer.PlayerStateChanged += OnPlayerStateChanged;
            MessagingCenter.Subscribe<object>(this, Constants.ReleasePlayer, OnRelease);
            MessagingCenter.Subscribe<object>(this, Constants.VolumeUp, OnVolumeUp);
            MessagingCenter.Subscribe<object>(this, Constants.VolumeDown, OnVolumeDown);
            MessagingCenter.Subscribe<object>(this, Constants.VolumeMute, OnVolumeMute);
            SetNativeControl(_vlcVideoPlayer);
        }

      

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "VideoSource")
            {
                var channelStr = ((VlcPlayerView)sender).VideoSource;
                if (channelStr != null && channelStr.Contains("\r")) {
                    channelStr = channelStr.Remove(channelStr.IndexOf("\r"), "\r".Length);
                }
                _uri = Android.Net.Uri.Parse(channelStr);
                if (_vlcVideoPlayer != null) _vlcVideoPlayer.Play(_uri);
            }
            if (e.PropertyName == "IsHardwareDecoding")
            {
                var isHardwareDecoing = ((VlcPlayerView)sender).IsHardwareDecoding;
                if (_vlcVideoPlayer != null)
                {
                    _vlcVideoPlayer.Stop();
                    _vlcVideoPlayer.SetHardwareDecoding(isHardwareDecoing);
                    _vlcVideoPlayer.Play(_uri);
                }
            }


        }

        private void OnVolumeMute(object obj)
        {
            _vlcVideoPlayer.SetVolume(0);
        }

        private void OnVolumeDown(object obj)
        {
            _vlcVideoPlayer.SetVolume(_vlcVideoPlayer.Volume - 10);
        }

        private void OnVolumeUp(object obj)
        {
            _vlcVideoPlayer.SetVolume(_vlcVideoPlayer.Volume + 10);
        }

        private void OnPlayerStateChanged(object sender, PlayerState state)
        {
            Toast.MakeText(Context, $"{state}", ToastLength.Short).Show();
        }


        private void OnRelease(object obj)
        {
            _vlcVideoPlayer.Release();

            _vlcVideoPlayer.PlayerStateChanged -= OnPlayerStateChanged;
            MessagingCenter.Unsubscribe<object>(this, Constants.ReleasePlayer);
            MessagingCenter.Unsubscribe<object>(this, Constants.VolumeUp);
            MessagingCenter.Unsubscribe<object>(this, Constants.VolumeDown);
            MessagingCenter.Unsubscribe<object>(this, Constants.VolumeMute);
            _vlcVideoPlayer = null;
        }
    }
}