using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Org.Videolan.Libvlc;
using Uri = Android.Net.Uri;

namespace Afaq.IPTV.Droid.Players.VlcPlayer
{
    public enum PlayerState
    {
        Idle = 0, Opening = 1, Playing = 3, Paused = 4, Stopping = 5, Ended = 6, Error = 7
    }
    public sealed class VlcVideoPlayer : SurfaceView
    {
        private readonly MediaPlayer _mediaPlayer;
        private LibVLCLibVLC _libvlc;
        private MediaLibVLC _media;
        private PlayerState _playerState;
        private Uri _latestUri; 
        public event EventHandler<PlayerState> PlayerStateChanged;

        public VlcVideoPlayer(IntPtr javaReference, JniHandleOwnership transfer) : base(javaReference, transfer)
        {
            
        }

        public VlcVideoPlayer(Context context) : base(context)
        {
            LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                ViewGroup.LayoutParams.WrapContent);
            _libvlc = new LibVLCLibVLC();
            _mediaPlayer = new MediaPlayer(_libvlc);
            _mediaPlayer.VLCVout.SetVideoView(this);
            _mediaPlayer.VLCVout.AttachViews();
            PlayerState = (PlayerState)_mediaPlayer.PlayerState;
            MonitorPlayerState();
        }

        public VlcVideoPlayer(Context context, IAttributeSet attrs) : base(context, attrs)
        {
        }

        public VlcVideoPlayer(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
        }

        public VlcVideoPlayer(Context context, IAttributeSet attrs, int defStyleAttr, int defStyleRes)
            : base(context, attrs, defStyleAttr, defStyleRes)
        {
        }

        public PlayerState PlayerState
        {
            get { return _playerState; }
            set
            {
                if (Equals(_playerState, value))
                    return;

                _playerState = value;
                PlayerStateChanged?.Invoke(this,value);
            }
        }

        public MediaLibVLC Media
        {
            get { return _media; }
            set
            {
                _media = value;
                if (_mediaPlayer != null)
                {
                    _mediaPlayer.Media = value;
                }
            }
        }

        public bool IsPlaying => _mediaPlayer != null && _mediaPlayer.IsPlaying;
        public bool IsDisposed { get; set; }
        public void Stop()
        {
            if (IsDisposed) {
                return;
            }
            _mediaPlayer?.Stop();
        }

        public void SetMedia(Uri media)
        {
            if (IsDisposed) {
                return;
            }
            if (_mediaPlayer == null) return;
            _mediaPlayer.Media = new MediaLibVLC(_libvlc, media);
        }

        public void Play()
        {
            if (IsDisposed) {
                return;
            }
            _mediaPlayer?.Play();
        }

        public void Play(Uri media)
        {
            if (IsDisposed) {
                return;
            }
            if (_mediaPlayer == null) return;
            _mediaPlayer.Media = new MediaLibVLC(_libvlc, media);
            Media = _mediaPlayer.Media;
            _latestUri = media;
            Play();
        }

        public void Release()
        {
            if (IsDisposed) {
                return;
            }
            if (_mediaPlayer == null || _mediaPlayer.IsReleased)
            {
                return;
            }

            _mediaPlayer.Stop();
            _mediaPlayer.VLCVout.DetachViews();
            _mediaPlayer.Release();
            _libvlc?.Release();
            _libvlc = null;
            IsDisposed = true;
        }

        public void EventHardwareAccelerationError()
        {
            Release();
            Toast.MakeText(Context, "Error with hardware acceleration", ToastLength.Long).Show();
        }

        private async void  MonitorPlayerState()
        {
            while (true)
            {
                await Task.Delay(500);
                if (_mediaPlayer == null) continue;
                if (IsDisposed)
                {
                    return;
                }
                PlayerState = (PlayerState)_mediaPlayer.PlayerState;
                if (PlayerState != PlayerState.Ended) continue;
                Stop();
                Play( _latestUri);
            }
        }
    }
}