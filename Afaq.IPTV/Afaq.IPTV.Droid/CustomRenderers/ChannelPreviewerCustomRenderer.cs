//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Linq;
//using System.Text;
//using Afaq.IPTV.Controls;
//using Afaq.IPTV.Droid.CustomRenderers;
//using Android.App;
//using Android.Content;
//using Android.OS;
//using Android.Runtime;
//using Android.Views;
//using Android.Widget;
//using Xam.Plugins.VideoPlayer;
//using Xamarin.Forms;
//using Xamarin.Forms.Platform.Android;


//[assembly: ExportRenderer(typeof(ChannelPreviewer), typeof(ChannelPreviewerCustomRenderer))]
//namespace Afaq.IPTV.Droid.CustomRenderers
//{
//    public class ChannelPreviewerCustomRenderer:VideoPlayerRenderer
//    {
//        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
//        {
//            base.OnElementChanged(e);
//            MessagingCenter.Subscribe<object>(this,"ReleasePreviewer",OnReleasePreviewer);
//            MessagingCenter.Subscribe<object>(this, "ShowPreviewer", OnShowPreviewer);
//        }

//        private void OnShowPreviewer(object obj)
//        {
//            if (Control != null) Control.Visibility = ViewStates.Visible;
//        }

//        private void OnReleasePreviewer(object obj)
//        {
//            if (Control != null)
//            {
//                Control.Suspend();
//                Control.Visibility = ViewStates.Invisible;
//            }
//        }

     
//    }
//}