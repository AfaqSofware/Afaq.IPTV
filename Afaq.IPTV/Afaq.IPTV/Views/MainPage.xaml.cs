using System;
using System.Collections.Generic;
using System.ComponentModel;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class MainPage
    {
        private readonly MainPageViewModel _viewModel;

        public MainPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, OnMoveUp);
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, OnMoveDown);
            MessagingCenter.Subscribe<object>(this, Constants.MoveLeft, OnMoveLeft);
            MessagingCenter.Subscribe<object>(this, Constants.MoveRight, OnMoveRight);
            MessagingCenter.Subscribe<object>(this, Constants.EnterKey, OnEnter);
            MessagingCenter.Subscribe<object>(this, Constants.DelKey, OnDelete);

            MessagingCenter.Subscribe<object, string>(this, Constants.KeyEntered, OnKeyEntered);
            _viewModel = (MainPageViewModel) BindingContext;
        }

        private void OnMoveRight(object obj)
        {
            var index = Children.IndexOf(CurrentPage);
            if (index >= Children.Count - 1) return;
            index++;
            CurrentPage = Children[index];
        }

        private void OnMoveLeft(object obj)
        {
            var index = Children.IndexOf(CurrentPage);
            if (index <= 0) return;
            index--;
            CurrentPage = Children[index];
        }

        private void OnDelete(object obj)
        {
            foreach (var channelList in _viewModel.ChannelLists)
            {
                if (channelList.Name != CurrentPage.Title) continue;
                var oldtext = channelList.SearchKey;
                if (string.IsNullOrEmpty(oldtext))
                {
                    return;
                }
                var newText = oldtext.Remove(oldtext.Length - 1);
                channelList.SearchKey = newText;
            }
        }

        private void OnKeyEntered(object arg1, string key)
        {
            foreach (var channelList in _viewModel.ChannelLists)
            {
                if (channelList.Name == CurrentPage.Title)
                {
                    channelList.SearchKey += key;
                }
            }
        }

        private void OnEnter(object obj)
        {
            foreach (var channelList in _viewModel.ChannelLists)
            {
                if (channelList.Name == CurrentPage.Title)
                {
                    channelList.PlaySelectedChannel();
                }
            }
        }

        private void OnMoveDown(object obj)
        {
            if (_viewModel.ChannelLists == null) return;
            foreach (var channelList in _viewModel.ChannelLists)
            {
                if (channelList.Name == CurrentPage.Title)
                {
                    channelList.MoveSelectionDown(null);
                }
            }
        }

        private void OnMoveUp(object obj)
        {
            if (_viewModel.ChannelLists == null) return;
            foreach (var channelList in _viewModel.ChannelLists)
            {
                if (channelList.Name == CurrentPage.Title)
                {
                    channelList.MoveSelectionUp(null);
                }
            }
        }

        private void ChannelList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            if (_viewModel.ChannelLists == null) return;
            foreach (var channelList in _viewModel.ChannelLists)
            {
                if (channelList.Name == CurrentPage.Title)
                {
                    channelList.PlaySelectedChannel();
                }
            }
        }

        private void MyChannelList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lstView = (ListView) sender;

            lstView.ScrollTo(lstView.SelectedItem, ScrollToPosition.Center, false);
        }


        private void MainPage_OnAppearing(object sender, EventArgs e)
        {
            _viewModel.SetScreenOrientation();
        }
    }
}