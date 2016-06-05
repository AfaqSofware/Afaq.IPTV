using System;
using System.Collections.Generic;
using System.ComponentModel;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class MainPagePhone
    {
        private MainPagePhoneViewModel _viewModel;

        public MainPagePhone()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, "MoveUp", OnMoveUp);
            MessagingCenter.Subscribe<object>(this, "MoveDown", OnMoveDown);
            MessagingCenter.Subscribe<object>(this, "MoveLeft", OnMoveLeft);
            MessagingCenter.Subscribe<object>(this, "MoveRight", OnMoveRight);
            MessagingCenter.Subscribe<object>(this, "Enter", OnEnter);
            MessagingCenter.Subscribe<object>(this, "DelKey", OnDelete);

            MessagingCenter.Subscribe<object, string>(this, "KeyEntered", OnKeyEntered);
            _viewModel = (MainPagePhoneViewModel) BindingContext;


        }

        private void OnMoveRight(object obj)
        {
            var index = Children.IndexOf(CurrentPage);
            if (index < Children.Count - 1)
            {
                index++;
                CurrentPage = Children[index];
            }
        }

        private void OnMoveLeft(object obj)
        {
            var index = Children.IndexOf(CurrentPage);
            if (index > 0)
            {
                index--;
                CurrentPage = Children[index];
            }
        }

        private void OnDelete(object obj)
        {
            foreach (var channelList in _viewModel.ChannelLists)
            {
                if (channelList.Name == CurrentPage.Title)
                {
                    var oldtext = channelList.SearchKey;
                    if (string.IsNullOrEmpty(oldtext))
                    {
                        return;
                    }
                    var newText = oldtext.Remove(oldtext.Length - 1);
                    channelList.SearchKey = newText;
                }
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
            if (_viewModel.ChannelLists != null)
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
            if (_viewModel.ChannelLists != null)
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
            if (_viewModel.ChannelLists != null)
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
            var lstView = (ListView)sender;

            lstView.ScrollTo(lstView.SelectedItem, ScrollToPosition.Center, false);
        }

        private void MainPagePhone_OnAppearing(object sender, EventArgs e)
        {
             _viewModel.SetScreenOrientation();
        }
    }
}