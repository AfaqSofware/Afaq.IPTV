using System;
using Afaq.IPTV.Models;
using Xamarin.Forms;

namespace Afaq.IPTV.Views.PartialViews
{
    public partial class MainTabletView
    {

        public MainTabletView()
        {
            InitializeComponent();
        }

        private void MyChannelList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lstView = (ListView) sender;

            lstView.ScrollTo(lstView.SelectedItem, ScrollToPosition.Center, false);
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            ((ChannelList)BindingContext).PlaySelectedChannel();
        }
    }
}