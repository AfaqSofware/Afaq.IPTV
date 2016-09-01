using System;
using System.Threading.Tasks;
using Android.Content;
using Android.Views;
using Android.Widget;
using Org.Videolan.Libvlc;
using Uri = Android.Net.Uri;

namespace Afaq.IPTV.Droid.Players.VlcPlayer
{
    public enum PlayerState
    {
        Idle = 0,
        Opening = 1,
        Playing = 3,
        Paused = 4,
        Stopping = 5,
        Ended = 6,
        Error = 7
    }

    public sealed class VlcVideoPlayer : SurfaceView
    {
        private readonly MediaPlayer _mediaPlayer;
        private Uri _latestUri;
        private LibVLCLibVLC _libvlc;
        private MediaLibVLC _media;
        private PlayerState _playerState;
        private int _volume;


        public VlcVideoPlayer(Context context) : base(context)
        {
            try
            {
                LayoutParameters = new ViewGroup.LayoutParams(ViewGroup.LayoutParams.WrapContent,
                    ViewGroup.LayoutParams.WrapContent);
                _libvlc = new LibVLCLibVLC(context);

                _mediaPlayer = new MediaPlayer(_libvlc);
                _mediaPlayer.VLCVout.SetVideoView(this);
                _mediaPlayer.VLCVout.AttachViews();
                PlayerState = (PlayerState) _mediaPlayer.PlayerState;
                Volume = 50;
                MonitorPlayerState();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public int Volume
        {
            get { return _volume; }
            set
            {
                if (value > 100 || value < 0)
                {
                    return;
                }
                _volume = value;
            }
        }

        public PlayerState PlayerState
        {
            get { return _playerState; }
            set
            {
                if (Equals(_playerState, value))
                    return;

                _playerState = value;
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
            try
            {
                if (IsDisposed)
                {
                    return;
                }
                _mediaPlayer?.Stop();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public void SetMedia(Uri media)
        {
            try
            {
                if (IsDisposed)
                {
                    return;
                }
                if (_mediaPlayer == null) return;
                _mediaPlayer.Media = new MediaLibVLC(_libvlc, media);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public void Play()
        {
            try
            {
                if (IsDisposed)
                {
                    return;
                }
                _mediaPlayer.SetVolume(Volume);
                _mediaPlayer?.Play();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        /// <summary>
        /// </summary>
        /// <param name="media">Its what is going to be played</param>
        public void Play(Uri media, bool isKeepMedia = true)
        {
            try
            {
                if (IsDisposed)
                {
                    return;
                }
                if (_mediaPlayer == null) return;
                _mediaPlayer.Media = new MediaLibVLC(_libvlc, media);

                Media = _mediaPlayer.Media;
                Media.SetHWDecoderEnabled(true, true);
                if (isKeepMedia)
                {
                    _latestUri = media;
                }            
                SetVolume(Volume);
                Play();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public void SetVolume(int level)
        {
            try
            {
                Volume = level;
                _mediaPlayer.SetVolume(level);
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public void Release()
        {
            try
            {
                if (IsDisposed)
                {
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
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        public void EventHardwareAccelerationError()
        {
            try
            {
                Release();
                Toast.MakeText(Context, "Error with hardware acceleration", ToastLength.Long).Show();
            }
            catch (Exception ex)
            {
                Toast.MakeText(Context, ex.Message, ToastLength.Long).Show();
            }
        }

        private async void MonitorPlayerState()
        {
            while (true)
            {
                await Task.Delay(500);
                if (_mediaPlayer == null) continue;
                if (IsDisposed)
                {
                    return;
                }
                PlayerState = (PlayerState) _mediaPlayer.PlayerState;
                if (PlayerState != PlayerState.Ended) continue;
                Play(Uri.Parse("https://picarto.tv/user_data/usrimg/cutesatan/offlineimage.png"), false);
                await Task.Delay(3000);
                Stop();
                Play(_latestUri);
            }
        }

        public void SetHardwareDecoding(bool isHardwareDecoing)
        {
            _media.SetHWDecoderEnabled(isHardwareDecoing, isHardwareDecoing);
        }
    }
}