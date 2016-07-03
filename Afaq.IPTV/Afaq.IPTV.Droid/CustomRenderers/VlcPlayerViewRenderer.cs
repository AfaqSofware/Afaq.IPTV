using System;
using System.ComponentModel;
using Afaq.IPTV.Controls;
using Afaq.IPTV.Droid.CustomRenderers;
using Afaq.IPTV.Droid.Players.VlcPlayer;
using Afaq.IPTV.Helpers;
using Android.Net;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(VlcPlayerView), typeof(VlcPlayerViewRenderer))]

namespace Afaq.IPTV.Droid.CustomRenderers
{
    public class VlcPlayerViewRenderer : ViewRenderer
    {
        
        private VlcVideoPlayer _vlcVideoPlayer;

        private void OnPlay(object arg1, string media)
        {
            _vlcVideoPlayer.Play(Android.Net.Uri.Parse(media));
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
         
            var vlcPlayerView = Element as VlcPlayerView;

            if ((vlcPlayerView == null) || (e.OldElement != null)) return;
            _vlcVideoPlayer = new VlcVideoPlayer(Context);
            _vlcVideoPlayer.PlayerStateChanged += OnPlayerStateChanged;
            MessagingCenter.Subscribe<object, string>(this, "Play", OnPlay);
            MessagingCenter.Subscribe<object>(this, Constants.ReleasePlayer, OnRelease);
            SetNativeControl(_vlcVideoPlayer);

        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName != "VideoSource") return;
            var channelStr = ((VlcPlayerView) sender).VideoSource;
            if (channelStr.Contains("\r"))
            {
                channelStr = channelStr.Remove(channelStr.IndexOf("\r"), "\r".Length); 
            }
            var uri = Android.Net.Uri.Parse(channelStr);
            _vlcVideoPlayer.Play(uri);

        }

        private void OnPlayerStateChanged(object sender, PlayerState state)
        {
            Toast.MakeText(Context, $"{state}", ToastLength.Short).Show();
        }

        private void OnRelease(object obj)
        {
            _vlcVideoPlayer.Release();
            _vlcVideoPlayer.PlayerStateChanged -= OnPlayerStateChanged;
            MessagingCenter.Unsubscribe<object, string>(this, "Play");
            MessagingCenter.Unsubscribe<object>(this, Constants.ReleasePlayer);
            _vlcVideoPlayer = null;
        }
    }
}