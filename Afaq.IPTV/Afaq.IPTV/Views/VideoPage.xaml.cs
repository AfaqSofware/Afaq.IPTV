using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class VideoPage
    {
        private readonly VideoPageViewModel _viewModel; 
        public VideoPage()
        {
            InitializeComponent();
            _viewModel = (VideoPageViewModel) BindingContext; 

        }


        protected override void OnAppearing()
        {
            base.OnAppearing();
            MessagingCenter.Subscribe<object>(this, Constants.AppPaused, OnPaused);
            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, o => { _viewModel.MoveSelectionUp(); });
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, o => { _viewModel.MoveSelectionDown(); });
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();
            MessagingCenter.Unsubscribe<object>(this, Constants.AppPaused);
            MessagingCenter.Unsubscribe<object>(this, Constants.MoveUp);
            MessagingCenter.Unsubscribe<object>(this, Constants.MoveDown);
            MessagingCenter.Send<object>(this, Constants.ReleasePlayer);
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            if (width<height) //Portrait
            {
                VideoPlayer.HeightRequest = height/3; 
            }
            else //Lanscape
            {
                VideoPlayer.HeightRequest = height;
            }
            base.OnSizeAllocated(width, height);
        }

        private void OnPaused(object obj)
        {
            SendBackButtonPressed();
        }

    }
}