using System;
using Afaq.IPTV.Helpers;
using Xamarin.Forms;

namespace Afaq.IPTV.Views.PartialViews
{
    public partial class UserPasswordLoginView
    {
        public UserPasswordLoginView()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, OnMoveUp);
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, OnMoveDown);
        }

        //protected override void OnAppearing()
        //{
        //    _viewModel.CanLogin = true;
        //    labelSerial.Text = Application.Current.Properties["Serial"].ToString();
        //    if (!LoginButton.IsFocused) {
        //        LoginButton.Focus();
        //    }
        //    base.OnAppearing();
        //}

        //protected override void OnDisappearing()
        //{
        //    _viewModel.CanLogin = false;
        //    base.OnDisappearing();
        //}

        private void OnMoveDown(object obj)
        {
            if (EntryUserName.IsFocused)
            {
                EntryPassword.Focus();
            }
            else
            {
                if (EntryPassword.IsFocused)
                {
                    SwitchRememberPassword.Focus();
                }
                else
                {
                    if (SwitchRememberPassword.IsFocused)
                    {
                        //        SwitchAutoLogin.Focus();
                    }
                    else
                    {
                        //if (SwitchAutoLogin.IsFocused) {
                        //    LoginButton.Focus();
                        //} else {
                        EntryUserName.Focus();
                    }
                }
            }
        }

        private void OnMoveUp(object obj)
        {
            if (LoginButton.IsFocused) {
           //     SwitchAutoLogin.Focus();
            } else {
                //if (SwitchAutoLogin.IsFocused) {
                //    SwitchRememberMe.Focus();
                //} else 
                {
                    if (SwitchRememberPassword.IsFocused) {
                        EntryPassword.Focus();
                    } else {
                        if (EntryPassword.IsFocused) {
                            EntryUserName.Focus();
                        } else {
                            LoginButton.Focus();
                        }
                    }
                }
            }
        }

        private void EntryUserName_OnCompleted(object sender, EventArgs e)
        {
            EntryPassword.Focus();
        }

        private void EntryPassword_OnCompleted(object sender, EventArgs e)
        {
            LoginButton.Focus();
        }

    }
}
