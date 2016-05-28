using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Afaq.IPTV.Controls;
using Afaq.IPTV.Droid.CustomRenderer;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Octane.Xam.VideoPlayer;
using Octane.Xam.VideoPlayer.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly:ExportRenderer(typeof(EventlessVideoPlayer),typeof(EventlessVideoPlayerRenderer))]
namespace Afaq.IPTV.Droid.CustomRenderer
{
    public class EventlessVideoPlayerRenderer:VideoPlayerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<VideoPlayer> e)
        {
            base.OnElementChanged(e);
            if (Control != null)
            {
             
              
                Control.KeyPress += Control_KeyPress;
            }
        }

        private void Control_KeyPress(object sender, KeyEventArgs e)
        {

            if (e.Event.Action == KeyEventActions.Up) {
                return;
            }
            switch (e.KeyCode) {
                case Keycode.DpadUp:
                    MessagingCenter.Send<object>(this, "MoveUp");
                    break;
                case Keycode.DpadDown:
                    MessagingCenter.Send<object>(this, "MoveDown");
                    break;
                case Keycode.Enter:
                    MessagingCenter.Send<object>(this, "Enter");
                    break;
            }

        }
    }
}