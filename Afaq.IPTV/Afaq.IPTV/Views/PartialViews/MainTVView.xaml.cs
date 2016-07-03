using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afaq.IPTV.Models;
using Xamarin.Forms;

namespace Afaq.IPTV.Views.PartialViews
{
    public partial class MainTvView 
    {
        public MainTvView()
        {
            this.Focused += OnFocus;
            InitializeComponent();
        }

        private void OnFocus(object sender, FocusEventArgs e)
        {
            var x = 5; 
        }

        private void MyChannelList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var lstView = (ListView)sender;

            lstView.ScrollTo(lstView.SelectedItem, ScrollToPosition.Center, false);
        }

        private void ChannelList_OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            ((ChannelList)BindingContext).PlaySelectedChannel();
        }

        
    }
}
