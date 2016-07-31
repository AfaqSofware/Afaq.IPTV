using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Afaq.IPTV.Controls
{
    public class VitamioPlayerView:View
    {
        //public event EventHandler Release;

        /// <summary>
        /// The url source of the video.
        /// </summary>
        public static readonly BindableProperty VideoSourceProperty = BindableProperty.Create(nameof(VideoSource), typeof(string), typeof(VlcPlayerView), "");




        /// <summary>
        /// The url source of the video.
        /// </summary>
        public string VideoSource
        {
            get
            {
                return (string)GetValue(VideoSourceProperty);

            }
            set
            {
                SetValue(VideoSourceProperty, value);
            }
        }
    }
}
