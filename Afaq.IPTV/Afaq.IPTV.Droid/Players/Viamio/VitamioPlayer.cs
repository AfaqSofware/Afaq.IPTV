
//using System;
//using Android.Content;

//using IO.Vov.Vitamio;
//using VideoView = IO.Vov.Vitamio.Widget.VideoView;

//namespace Afaq.IPTV.Droid.Players.Viamio
//{
//    public class VitamioPlayer:VideoView,MediaPlayer.IOnPreparedListener , MediaPlayer.IOnBufferingUpdateListener , MediaPlayer.IOnCompletionListener, MediaPlayer.IOnErrorListener
//    {
//        public event EventHandler OnCompleted;
//        public event EventHandler<int> OnBufferingUpdated;
//        public event EventHandler<int> OnErrorEvent; 
//        public VitamioPlayer(Context p0) : base(p0)
//        {
           
//        }

//        public void OnBufferingUpdate(MediaPlayer player, int p1)
//        {
//            OnBufferingUpdated?.Invoke(player, p1);
//        }

//        public void OnCompletion(MediaPlayer player)
//        {
//            OnCompleted?.Invoke(player, null);
//        }

//        public void OnPrepared(MediaPlayer player)
//        {
//           player.SetPlaybackSpeed(1);
//        }

//        public bool OnError(MediaPlayer player, int p1, int p2)
//        {
//            OnErrorEvent?.Invoke(player,p1);
//            return true; 
//        }

      
//    }
//}