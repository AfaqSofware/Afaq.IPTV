using System;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Prism.Navigation;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Afaq.IPTV.Views
{
    public partial class LoginPage:INavigationAware
    {
        private readonly LoginPageViewModel _viewModel;

        public LoginPage()
        {
            InitializeComponent();

            _viewModel = (LoginPageViewModel) BindingContext;
            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, OnMoveUp);
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, OnMoveDown);
        }

        protected override void OnAppearing()
        {
            _viewModel.CanLogin = true;
            if (!LoginButton.IsFocused)
            {
                LoginButton.Focus();
            }
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _viewModel.CanLogin = false;
            base.OnDisappearing();
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);

            if (width > height)
            {
                outerStackLayout.Orientation = StackOrientation.Horizontal;
            }
            else
            {
                outerStackLayout.Orientation = StackOrientation.Vertical;
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

        private async void AutoLogin()
        {
            if (_viewModel.IsAutoLogin)
            {
                await _viewModel.LoginCommand.Execute();
            }
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters != null && (bool)parameters["IsAutoLogin"]) {
                AutoLogin();
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


        private async void OnThreeMonthsSubsciptionTapped(object sender, EventArgs e)
        {
            var page = new WebBrowsingPage("http://arabictv.ml/index.php?route=product/product&product_id=52");
            await Navigation.PushPopupAsync(page);

        }

        private async void OnOneYearSubscriptionTapped(object sender, EventArgs e)
        {
            var page = new WebBrowsingPage("http://arabictv.ml/index.php?route=product/product&product_id=61");                                           
            await Navigation.PushPopupAsync(page);
        }
    }
}