﻿using Afaq.IPTV.Models;
using Xamarin.Forms;

namespace Afaq.IPTV.Views.PartialViews
{
    public partial class MainPhoneView
    {

        public MainPhoneView()
        {
            InitializeComponent();
        }

        private void MyChannelList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lstView = (ListView) sender;

            lstView.ScrollTo(lstView.SelectedItem, ScrollToPosition.Center, false);
        }

        private void ChannelList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ChannelList) BindingContext).PlaySelectedChannel();
        }
    }
}