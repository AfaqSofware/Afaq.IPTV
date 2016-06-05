using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Afaq.IPTV.Services;
using Afaq.IPTV.ViewModels;
using Afaq.IPTV.Views;
using Microsoft.Practices.Unity;
using Prism;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;
using Xamarin.Forms;

namespace Afaq.IPTV
{
    public class App : PrismApplication
    {
        public App()
        {
          
        }
  
        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }

        protected override void OnInitialized()
        {
            NavigationService.NavigateAsync("LoginPage");
        }

        protected override void RegisterTypes()
        {
            Container.RegisterType<IAuthenticationService, AuthenticationService>();
            Container.RegisterType<IChannelService, M3UChannelService>();
           
            //Container.RegisterInstance(NavigationService, new ContainerControlledLifetimeManager());
            //Container.RegisterType<IVideoPageViewModel, VideoPageViewModel>(new ContainerControlledLifetimeManager());
            //Container.RegisterType<IMainPagePhoneViewModel, MainPagePhoneViewModel>(
            //    new ContainerControlledLifetimeManager());

            Container.RegisterTypeForNavigation<VideoPage>();
            Container.RegisterTypeForNavigation<LoginPage>();


            if (Device.Idiom == TargetIdiom.Phone) {
                Container.RegisterTypeForNavigation<MainPagePhone>("MainPage");
            } else {
                Container.RegisterTypeForNavigation<MainPagePhone>("MainPage");
            }


        }
    }
}
