using System;
using System.ComponentModel;
using Afaq.IPTV.Controls;
using Afaq.IPTV.Droid.CustomRenderers;
using Afaq.IPTV.Droid.Players.VlcPlayer;
using Afaq.IPTV.Helpers;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Uri = Android.Net.Uri;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(VlcPreviewer), typeof(VlcPreviewerRenderer))]

namespace Afaq.IPTV.Droid.CustomRenderers
{
    public class VlcPreviewerRenderer : ViewRenderer
    {
        private bool _isHidden;
        private Uri _uri;

        private VlcVideoPlayer _vlcVideoPlayer;

        public VlcPreviewerRenderer()
        {
            //Hide control bar 
            RootView.SystemUiVisibility = StatusBarVisibility.Hidden;

            _isHidden = false;
            _vlcVideoPlayer = new VlcVideoPlayer(Context);
            _vlcVideoPlayer.PlayerStateChanged += OnPlayerStateChanged;
            MessagingCenter.Subscribe<object>(this, Constants.HidePlayer, OnHidePlayer);
            MessagingCenter.Subscribe<object>(this, Constants.ShowPlayer, OnShowPlayer);
            MessagingCenter.Subscribe<object>(this, Constants.VolumeUp, OnVolumeUp);
            MessagingCenter.Subscribe<object>(this, Constants.VolumeDown, OnVolumeDown);
            MessagingCenter.Subscribe<object>(this, Constants.VolumeMute, OnMutePlayer);
            MessagingCenter.Subscribe<object>(this, Constants.StopPlayer, OnStopPlayer);
        }

        protected override void Dispose(bool disposing)
        {
            _vlcVideoPlayer.PlayerStateChanged -= OnPlayerStateChanged;
            MessagingCenter.Unsubscribe<object>(this, Constants.HidePlayer);
            MessagingCenter.Unsubscribe<object>(this, Constants.ShowPlayer);
            MessagingCenter.Unsubscribe<object>(this, Constants.VolumeUp);
            MessagingCenter.Unsubscribe<object>(this, Constants.VolumeDown);
            MessagingCenter.Unsubscribe<object>(this, Constants.VolumeMute);
            MessagingCenter.Unsubscribe<object>(this, Constants.StopPlayer);
            base.Dispose(disposing);
            GC.Collect();
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);
            SetNativeControl(_vlcVideoPlayer);
        }


        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == "VideoSource")
            {
                var channelStr = ((VlcPreviewer) sender).VideoSource;
                if (channelStr.Contains("\r"))
                {
                    channelStr = channelStr.Remove(channelStr.IndexOf("\r"), "\r".Length);
                }
                _uri = Uri.Parse(channelStr);
                if (_isHidden)
                {
                    return;
                }
                _vlcVideoPlayer.Play(_uri);
            }
            if (e.PropertyName == "IsHardwareDecoding")
            {
                var isHardwareDecoing = ((VlcPreviewer) sender).IsHardwareDecoding;
                _vlcVideoPlayer.Stop();
                _vlcVideoPlayer.SetHardwareDecoding(isHardwareDecoing);
                if (_isHidden)
                {
                    return;
                }
                _vlcVideoPlayer.Play(_uri);
            }
        }

        private void OnStopPlayer(object obj)
        {
            if (_vlcVideoPlayer.IsPlaying)
            {
                _vlcVideoPlayer.Stop();
            }
        }

        private void OnMutePlayer(object obj)
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

        private void OnShowPlayer(object obj)
        {
            _isHidden = false;
            _vlcVideoPlayer.Release();
            _vlcVideoPlayer = new VlcVideoPlayer(Context);
            SetNativeControl(_vlcVideoPlayer);
            if (_uri != null)
            {
                _vlcVideoPlayer.Play(_uri);
            }
        }

        private void OnHidePlayer(object obj)
        {
            _isHidden = true;
            _vlcVideoPlayer.Stop();
            _vlcVideoPlayer.Visibility = ViewStates.Invisible;
        }
    }
}