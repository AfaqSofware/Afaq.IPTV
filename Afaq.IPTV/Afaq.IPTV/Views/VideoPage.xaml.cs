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
            MessagingCenter.Subscribe<object>(this, Constants.AppPaused, OnPaused);

        }

        private void OnPaused(object obj)
        {
            SendBackButtonPressed();
        }

        private void VideoPage_OnDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Unsubscribe<object>(this, Constants.AppPaused);
            MessagingCenter.Send<object>(this, Constants.ReleasePlayer);
        }
    }
}