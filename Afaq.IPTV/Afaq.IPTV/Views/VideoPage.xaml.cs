using System;
using Afaq.IPTV.Controls;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class VideoPage
    {


        public VideoPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, "Paused", OnPaused);

        }

        private void OnPaused(object obj)
        {
            SendBackButtonPressed();
        }

        private void VideoPage_OnDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<object>(this, "Paused");
            MessagingCenter.Send<object>(this, Constants.ReleasePlayer);
        }

        protected override void OnSizeAllocated(double width, double height)
        {

            if (width > height)
            {

                VideoPlayer.Margin = new Thickness(0);
            }
            else
            {
                VideoPlayer.Margin = new Thickness(0, height/3, 0, height/3);
            }

            base.OnSizeAllocated(width, height);
        }
    }
}