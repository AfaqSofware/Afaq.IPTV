using Xamarin.Forms;

namespace Afaq.IPTV.Views.PartialViews
{
    /// <summary>
    /// ExoVideoPlayerView is a View which contains a MediaElement to play a video.
    /// </summary>
    public class VlcVideoPlayerView : View
    {
        //public event EventHandler Release;

        /// <summary>
        /// The url source of the video.
        /// </summary>
        public static readonly BindableProperty VideoSourceProperty = BindableProperty.Create(nameof(VideoSource),
            typeof(string), typeof(ExoVideoPlayerView), "");


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

        /// <summary>
        /// The scale format of the video which is in most cases 16:9 (1.77) or 4:3 (1.33).
        /// </summary>

        public static readonly BindableProperty VideoScaleProperty = BindableProperty.Create(nameof(VideoScale),
            typeof(double), typeof(ExoVideoPlayerView), 1.77);

        /// <summary>
        /// The scale format of the video which is in most cases 16:9 (1.77) or 4:3 (1.33).
        /// </summary>
        public double VideoScale
        {
            get
            {
                return (double)GetValue(VideoScaleProperty);
            }
            set
            {
                SetValue(VideoScaleProperty, value);
            }
        }
    }
}
