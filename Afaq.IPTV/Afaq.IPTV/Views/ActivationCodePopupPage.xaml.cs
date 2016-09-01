using System;
using Afaq.IPTV.ViewModels;

namespace Afaq.IPTV.Views
{
    public partial class ActivationCodePopupPage 
    {

        public ActivationCodePopupPage()
        {
            InitializeComponent();
         
            (BindingContext as ActivationCodePopupPageViewModel)?.Init("Default");
        }


        private void EntryActivationCode_OnCompleted(object sender, EventArgs e)
        {
            BtnAdd.Focus();
        }
    }
}
