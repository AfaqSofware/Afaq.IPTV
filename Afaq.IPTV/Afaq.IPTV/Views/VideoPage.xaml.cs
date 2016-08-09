using System;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class VideoPage
    {
        private VideoPageViewModel _viewModel; 
        public VideoPage()
        {
            InitializeComponent();
            _viewModel = (VideoPageViewModel) BindingContext; 
            MessagingCenter.Subscribe<object>(this, Constants.AppPaused, OnPaused);
            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, o => { _viewModel.MoveSelectionUp(); });
            MessagingCenter.Subscribe<object>(this, Constants.ChannelUp, o => { _viewModel.MoveSelectionUp(); });
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, o => { _viewModel.MoveSelectionDown(); });
            MessagingCenter.Subscribe<object>(this, Constants.ChannelDown, o => { _viewModel.MoveSelectionDown(); });
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