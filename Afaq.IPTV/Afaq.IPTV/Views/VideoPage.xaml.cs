
using System;
using Afaq.IPTV.Helpers;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class VideoPage
    {
        public VideoPage()
        {
            InitializeComponent();
        }

        private void VideoPage_OnDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Send<object, bool>(this,Constants.ReleasePlayer, true);
        }
    }
}