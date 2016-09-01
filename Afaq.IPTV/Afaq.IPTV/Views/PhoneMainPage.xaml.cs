using System;
using Afaq.IPTV.Events;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.Models;
using Afaq.IPTV.ViewModels;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class PhoneMainPage
    {
        private readonly IMainPageViewModel _viewModel;

        public PhoneMainPage(IMainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;

        }
        
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _viewModel.CanPlay = true;
            _viewModel.SetCinemaMode(false);
        }

        protected override bool OnBackButtonPressed()
        {       
            _viewModel.SignOut(); 
            return true;
        }

        private void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            _viewModel.IsMobile = true;
            _viewModel.PlayCurrentChannelCommand.Execute(CurrentPage.BindingContext);
        }
    }
}