using System;
using Afaq.IPTV.ViewModels;
using Rg.Plugins.Popup.Extensions;

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
            var page = new ActivationCodePage();
            await Navigation.PushPopupAsync(page);
        }
    }
}
