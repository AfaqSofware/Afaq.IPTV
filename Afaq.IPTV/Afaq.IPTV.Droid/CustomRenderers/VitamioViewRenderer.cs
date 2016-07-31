
//using System.ComponentModel;
//using Afaq.IPTV.Controls;
//using Afaq.IPTV.Droid.CustomRenderers;
//using Afaq.IPTV.Droid.Players.Viamio;
//using IO.Vov.Vitamio;
//using IO.Vov.Vitamio.Widget;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;
//using MediaPlayer = Org.Videolan.Libvlc.Media.MediaPlayer;
//using View = Xamarin.Forms.View;

//[assembly: ExportRenderer(typeof(VitamioPlayerView), typeof(VitamioViewRenderer))]
//namespace Afaq.IPTV.Droid.CustomRenderers
//{
//    public class VitamioViewRenderer:ViewRenderer
//    {
//        private VitamioPlayer _videoView; 
//        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
//        {
//            base.OnElementChanged(e);
//            _videoView = new VitamioPlayer(Context);
//            _videoView.SetHardwareDecoder(false);
//            SetNativeControl(_videoView);
//        }

//        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
//        {
//            base.OnElementPropertyChanged(sender, e);
//            if (e.PropertyName != "VideoSource") return;
//            var channelStr = ((VitamioPlayerView)sender).VideoSource;
//            if (channelStr.Contains("\r")) {
//                channelStr = channelStr.Remove(channelStr.IndexOf("\r"), "\r".Length);
//            }
//            _videoView.SetVideoPath(channelStr);
                
//        }
//    }
//}