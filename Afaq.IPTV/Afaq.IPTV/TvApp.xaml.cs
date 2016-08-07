using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afaq.IPTV.Services;
using Afaq.IPTV.ViewModels;
using Afaq.IPTV.Views;
using Microsoft.Practices.Unity;
using Prism.Navigation;
using Prism.Unity;
using Prism.Unity.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV
{
    public partial class TvApp 
    {


        protected override void OnInitialized()
        {
            InitializeComponent();

            NavigationService.NavigateAsync("LoginPage", new NavigationParameters { { "IsAutoLogin", true } });
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<INavigationService, UnityPageNavigationService>();
            Container.RegisterType<IMainPageViewModel, MainPageViewModel>();
            Container.RegisterType<IAuthenticationService, AuthenticationService>();
            Container.RegisterType<IChannelService, M3UChannelService>();

            Container.RegisterTypeForNavigation<VideoPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            Container.RegisterTypeForNavigation<TvMainPage>("MainPage");
        }
    }
}


