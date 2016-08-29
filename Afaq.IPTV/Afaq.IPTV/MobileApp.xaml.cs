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
    public partial class MobileApp
    {

        protected override void OnInitialized()
        {
            InitializeComponent();
          
            NavigationService.NavigateAsync("LoginPage", new NavigationParameters { { "IsAutoLogin", true } });
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IDbService, RealmDbService>();
            Container.RegisterType<INavigationService, UnityPageNavigationService>(); 
            Container.RegisterType<IMainPageViewModel, MainPageViewModel>();
            Container.RegisterType<IAuthenticationService, AuthenticationService>();
            Container.RegisterType<IChannelService, M3UChannelService>();

            Container.RegisterTypeForNavigation<VideoPage>();
            Container.RegisterTypeForNavigation<LoginPage>();
            if (Device.Idiom == TargetIdiom.Phone)
            {
                Container.RegisterTypeForNavigation<PhoneMainPage>("MainPage");
            }
            else
            {
                Container.RegisterTypeForNavigation<TvMainPage>("MainPage");
            }
            
        }
    }
}