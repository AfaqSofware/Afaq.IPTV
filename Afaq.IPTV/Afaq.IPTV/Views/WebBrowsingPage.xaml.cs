using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class WebBrowsingPage : PopupPage
    {
        private readonly string _url;

        public WebBrowsingPage(string url )
        {
            _url = url;

            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MyWebView.HeightRequest = Height / 1.5;
            MyWebView.Source = _url;
        }

        protected override bool OnBackButtonPressed()
        {
            //if (WebView.CanGoBack) {
            //    WebView.GoBack();
            //    return true;
            //}

            return base.OnBackButtonPressed();
        }

        private void OnClose(object sender, EventArgs e)
        {
            PopupNavigation.PopAsync();
        }
    }
}
