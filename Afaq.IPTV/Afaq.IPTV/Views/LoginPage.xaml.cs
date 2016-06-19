using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Afaq.IPTV.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly LoginPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();
            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, OnMoveUp);
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, OnMoveDown);
            _viewModel = (LoginPageViewModel) BindingContext;
            AutoLogin();
        }

        private async void AutoLogin()
        {
            if (_viewModel.IsAutoLogin)
            {
                await _viewModel.LoginCommand.Execute();
            }
        }

        protected override void OnAppearing()
        {
            if (!LoginButton.IsFocused)
            {
                LoginButton.Focus();
            }
        }

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
                    SwitchRememberMe.Focus();
                }
                else
                {
                    if (SwitchRememberMe.IsFocused)
                    {
                        SwitchAutoLogin.Focus();
                    }
                    else
                    {
                        if (SwitchAutoLogin.IsFocused)
                        {
                            LoginButton.Focus();
                        }
                        else
                        {
                            EntryUserName.Focus();
                        }
                    }
                }
            }
        }

        private void OnMoveUp(object obj)
        {
            if (LoginButton.IsFocused)
            {
                SwitchAutoLogin.Focus();
            }
            else
            {
                if (SwitchAutoLogin.IsFocused)
                {
                    SwitchRememberMe.Focus();
                }
                else
                {
                    if (SwitchRememberMe.IsFocused)
                    {
                        EntryPassword.Focus();
                    }
                    else
                    {
                        if (EntryPassword.IsFocused)
                        {
                            EntryUserName.Focus();
                        }
                        else
                        {
                            LoginButton.Focus();
                        }
                    }
                }
            }
        }
    }
}