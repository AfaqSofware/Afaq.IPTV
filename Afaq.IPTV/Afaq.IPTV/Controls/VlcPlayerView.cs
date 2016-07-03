﻿using Xamarin.Forms;

namespace Afaq.IPTV.Controls
{
    /// <summary>
    ///  VlcPlayerView is a View which contains a VlcPlayer to play a video.
    /// </summary>
    public class VlcPlayerView : View
    {
        //public event EventHandler Release;

        /// <summary>
        /// The url source of the video.
        /// </summary>
        public static readonly BindableProperty VideoSourceProperty = BindableProperty.Create(nameof(VideoSource),typeof(string), typeof(VlcPlayerView), "");




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