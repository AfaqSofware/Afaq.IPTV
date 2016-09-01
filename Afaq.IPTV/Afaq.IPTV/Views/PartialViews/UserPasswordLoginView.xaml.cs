using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace Afaq.IPTV.Views.PartialViews
{
    public partial class UserPasswordLoginView
    {
        public UserPasswordLoginView()
        {
            InitializeComponent();
            LoginButton.Focus();
        }


        public List<View> GetFocusableViews()
        {
            return new List<View> {EntryUserName, EntryPassword, SwitchRememberPassword, LoginButton};
        }

        private void EntryUserName_OnCompleted(object sender, EventArgs e)
        {
            EntryPassword.Focus();
        }
    }
}