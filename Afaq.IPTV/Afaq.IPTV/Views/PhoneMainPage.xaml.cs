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


        private void MainPage_OnAppearing(object sender, EventArgs e)
        {
            MessagingCenter.Send<object>(this, "ShowPreviewer");
            _viewModel.SetCinemaMode(false); 

        }

        protected override bool OnBackButtonPressed()
        {       
            _viewModel.SignOut(); 
            return true;
        }

        private void ChannelList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var channelListView = sender as ListView;
            if (channelListView != null) _viewModel.PlayCurrentChannelCommand.Execute(channelListView.SelectedItem);
        }
    }
}