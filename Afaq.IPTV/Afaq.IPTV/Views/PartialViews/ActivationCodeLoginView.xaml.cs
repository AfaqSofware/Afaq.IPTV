using System;
using System.Collections.Generic;
using Afaq.IPTV.ViewModels;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;

namespace Afaq.IPTV.Views.PartialViews
{
    public partial class ActivationCodeLoginView
    {
        public ActivationCodeLoginView()
        {
            InitializeComponent();
        }

        private async void ButtonManageActivationCode_OnClicked(object sender, EventArgs e)
        {
            var page = new ActivationCodePopupPage();
            await Navigation.PushPopupAsync(page);
        }

        public List<View> GetFocusableViews()
        {
            return new List<View> { ButtonManageActivationCode, ButtonNext};
        }
    }
}
