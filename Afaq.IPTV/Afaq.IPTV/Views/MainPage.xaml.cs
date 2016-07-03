using System;
using Afaq.IPTV.Events;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class MainPage
    {
        private IMainPageViewModel _viewModel;

        public MainPage(IMainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel; 
        }

        private void MainPage_OnAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Send<object>(this, "ShowPreviewer");

            if (Device.Idiom == TargetIdiom.Phone) {
             _viewModel.Aggregator.GetEvent<FullScreenEvent>().Publish(new FullScreenEventArgs { IsFullScreen = false, IsPhone = true });
            } else {
                _viewModel.Aggregator.GetEvent<FullScreenEvent>().Publish(new FullScreenEventArgs { IsFullScreen = false, IsPhone = false });
            }

        }


        private void MainPage_OnDisappearing(object sender, EventArgs e)
        {
            MessagingCenter.Send<object>(this, "ReleasePreviewer");
        }

        protected override bool OnBackButtonPressed()
        {
            try
            {
                _viewModel.NavigationService.NavigateAsync(new Uri("http://www.Afaq.com/LoginPage", UriKind.Absolute), new NavigationParameters { { "IsAutoLogin", false } }, true).Wait();
            }
            catch (Exception ex)
            {
                
                throw;
            }
          
            return true;
        }
    }
}