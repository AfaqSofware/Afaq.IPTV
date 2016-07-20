using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class MainPage
    {
        private MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            _viewModel = (MainPageViewModel) BindingContext;
            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, OnMoveUp);
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, OnMoveDown);
            MessagingCenter.Subscribe<object>(this, Constants.MoveLeft, OnMoveLeft);
            MessagingCenter.Subscribe<object>(this, Constants.MoveRight, OnMoveRight);
            MessagingCenter.Subscribe<object>(this, Constants.EnterKey, OnEnter);
            MessagingCenter.Subscribe<object>(this, Constants.DelKey, OnDelete);
            MessagingCenter.Subscribe<object, string>(this, Constants.KeyEntered, OnKeyEntered);
        }

        private void OnMoveRight(object obj)
        {
            _viewModel.GetNextChannelList();
        }

        private void OnMoveLeft(object obj)
        {
            _viewModel.GetPreviousChannelList();
        }

        private void OnDelete(object obj)
        {
            var oldtext = _viewModel.CurrentChannelList.SearchKey;
            var newText = oldtext.Remove(oldtext.Length - 1);
            _viewModel.CurrentChannelList.SearchKey = newText;
        }

        private void OnKeyEntered(object arg1, string key)
        {
            _viewModel.CurrentChannelList.SearchKey += key;
        }

     

        private void OnMoveDown(object obj)
        {
            _viewModel.CurrentChannelList?.MoveSelectionDown(null);
        }

        private void OnMoveUp(object obj)
        {
            _viewModel.CurrentChannelList?.MoveSelectionUp(null);
           
        }

        private void MyChannelList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lstView = (ListView)sender;

            lstView.ScrollTo(lstView.SelectedItem, ScrollToPosition.Center, false);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
           _viewModel.PlayChannel(_viewModel.CurrentChannel);
        }
        private void OnEnter(object obj)
        {
            _viewModel.PlayChannel(_viewModel.CurrentChannel);
        }
    }
}