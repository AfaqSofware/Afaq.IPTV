using System;
using System.Collections.Generic;
using System.Linq;
using Afaq.IPTV.Enums;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.ViewModels;
using Afaq.IPTV.Views.PartialViews;
using Prism.Navigation;
using Rg.Plugins.Popup.Extensions;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace Afaq.IPTV.Views
{
    public partial class LoginPage : INavigationAware
    {
        private readonly LoginPageViewModel _viewModel;
        private readonly List<View> _focusableViews; //this list is used to switch the focus on different controls 

        public LoginPage()
        {
            InitializeComponent();

            _viewModel = (LoginPageViewModel) BindingContext;
            _focusableViews = new List<View>();
            if (Settings.LoginViewType == LoginViewType.ActivationCodeView)
            {
                LoginFrame.Content = new ActivationCodeLoginView();
                var activationCodeLoginView = (ActivationCodeLoginView) LoginFrame.Content;
                _focusableViews.AddRange(activationCodeLoginView.GetFocusableViews());
            }
            else
            {
                LoginFrame.Content = new UserPasswordLoginView();
                var userPasswordLoginView = (UserPasswordLoginView) LoginFrame.Content;
                _focusableViews.AddRange(userPasswordLoginView.GetFocusableViews());
            }
            _focusableViews.Add(SwitchAutoLogin);
            _focusableViews.Add(btnSwitchMode);
            _focusableViews.Add(BtnSubscribe);
            _focusableViews.Add(BtnFreeTrial);

            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, OnMoveUp);
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, OnMoveDown);
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {
            //   Do nothing
        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {
            if (parameters == null) return;
            if ((bool) parameters["IsAutoLogin"])
            {
                AutoLogin();
            }
        }

        private void OnMoveDown(object obj)
        {
            var focusedIndex = _focusableViews.FindIndex(a => a.IsFocused);
            if (focusedIndex == _focusableViews.IndexOf(_focusableViews.Last()) || focusedIndex == -1)
            {
                _focusableViews[0].Focus();
            }
            else
            {
                _focusableViews[focusedIndex + 1].Focus();
            }
        }

        private void OnMoveUp(object obj)
        {
            var focusedIndex = _focusableViews.FindIndex(a => a.IsFocused);
            if (focusedIndex == -1 || focusedIndex == 0)
            {
                _focusableViews.Last().Focus();
            }

            else
            {
                _focusableViews[focusedIndex - 1].Focus();
            }
        }

        protected override void OnAppearing()
        {
            labelSerial.Text = Application.Current.Properties["Serial"].ToString();
            _viewModel.CanLoginUsernamePassword = true;
            base.OnAppearing();
        }

        protected override void OnDisappearing()
        {
            _viewModel.CanLoginUsernamePassword = false;
            base.OnDisappearing();
        }

        private void BtnSwitchMode_OnClicked(object sender, EventArgs e)
        {
            if (LoginFrame.Content is ActivationCodeLoginView)
            {
                var activationCodeLoginViews = ((ActivationCodeLoginView) LoginFrame.Content).GetFocusableViews();
                foreach (var view in activationCodeLoginViews)
                {
                    _focusableViews.Remove(view);
                }

                LoginFrame.Content = new UserPasswordLoginView();
                _focusableViews.InsertRange(0, ((UserPasswordLoginView) LoginFrame.Content).GetFocusableViews());
                Settings.LoginViewType = LoginViewType.UserNameView;
            }
            else
            {
                var usernamePasswordLoginViews = ((UserPasswordLoginView) LoginFrame.Content).GetFocusableViews();
                foreach (var view in usernamePasswordLoginViews)
                {
                    _focusableViews.Remove(view);
                }
                LoginFrame.Content = new ActivationCodeLoginView();
                _focusableViews.InsertRange(0, ((ActivationCodeLoginView) LoginFrame.Content).GetFocusableViews());
                Settings.LoginViewType = LoginViewType.ActivationCodeView;
            }
        }

        private async void btnSubscribe_OnClicked(object sender, EventArgs e)
        {
            var page = new WebBrowsingPage("http://arabictv.ml/index.php?route=product/product&path=57&product_id=51");
            await Navigation.PushPopupAsync(page);
        }

        private async void BtnFreeTrial_OnClicked(object sender, EventArgs e)
        {
            var page = new WebBrowsingPage("http://arabictv.ml/index.php?route=product/product&path=57&product_id=50");
            await Navigation.PushPopupAsync(page);
        }

        private async void AutoLogin()
        {
            if (!_viewModel.IsAutoLogin) return;
            if (LoginFrame.Content is ActivationCodeLoginView)
            {
                await _viewModel.LoginActivationCodesCommand.Execute();
            }
            else
            {
                await _viewModel.LoginUsernamePasswordCommand.Execute();
            }
        }
    }
}