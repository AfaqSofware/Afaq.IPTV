using System;
using Afaq.IPTV.Events;
using Afaq.IPTV.Helpers;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using Prism.Events;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace Afaq.IPTV.Droid
{
    [Activity(Label = "Afaq.IPTV", Icon = "@drawable/icon", MainLauncher = true,
        ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsApplicationActivity
    {



        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;


        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);

            Forms.Init(this, bundle);
            var application = new App();
            _container = application.Container;
            LoadApplication(application);
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<FullScreenEvent>().Subscribe(OnFullScreenEvent);
        }


        private void OnFullScreenEvent(FullScreenEventArgs fullScreenEventArgs)
        {
            if (fullScreenEventArgs.IsFullScreen)
            {
                if (fullScreenEventArgs.IsPhone)
                {
                    RequestedOrientation = ScreenOrientation.Landscape;
                }
            }
            else
            {
                if (fullScreenEventArgs.IsPhone)
                {
                    RequestedOrientation = ScreenOrientation.Portrait;
                }
            }
        }



        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            switch (e.KeyCode) {
                case Keycode.DpadUp:
                    MessagingCenter.Send<object>(this, Constants.MoveUp);
                    break;

                case Keycode.DpadDown:
                    MessagingCenter.Send<object>(this, Constants.MoveDown);
                    break;

                case Keycode.DpadLeft:
                    MessagingCenter.Send<object>(this, Constants.MoveLeft);
                    break;

                case Keycode.DpadRight:
                    MessagingCenter.Send<object>(this, Constants.MoveRight);
                    break;
                case Keycode.Enter:
                    MessagingCenter.Send<object>(this, Constants.EnterKey);
                    break;

                case Keycode.Num0:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "0");
                    break;
                case Keycode.Num1:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "1");
                    break;
                case Keycode.Num2:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "2");
                    break;
                case Keycode.Num3:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "3");
                    break;
                case Keycode.Num4:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "4");
                    break;
                case Keycode.Num5:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "5");
                    break;
                case Keycode.Num6:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "6");
                    break;
                case Keycode.Num7:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "7");
                    break;
                case Keycode.Num8:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "8");
                    break;
                case Keycode.Num9:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "9");
                    break;
                case Keycode.A:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "A");
                    break;
                case Keycode.B:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "B");
                    break;
                case Keycode.C:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "C");
                    break;
                case Keycode.ChannelDown:
                    MessagingCenter.Send<object>(this, Constants.ChannelDown);
                    break;
                case Keycode.ChannelUp:
                    MessagingCenter.Send<object>(this, Constants.ChannelUp);
                    break;
                case Keycode.D:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "D");
                    break;
                case Keycode.E:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "E");
                    break;
                case Keycode.F:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "F");
                    break;
                case Keycode.G:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "G");
                    break;
                case Keycode.H:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "H");
                    break;
                case Keycode.I:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "I");
                    break;
                case Keycode.J:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "J");
                    break;
                case Keycode.K:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "K");
                    break;
                case Keycode.L:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "L");
                    break;
                case Keycode.M:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "M");
                    break;
                case Keycode.Menu:
                    MessagingCenter.Send<object>(this, Constants.MenuKey);
                    break;
                case Keycode.N:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "N");
                    break;
                case Keycode.Numpad0:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "0");
                    break;
                case Keycode.Numpad1:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "1");
                    break;
                case Keycode.Numpad2:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "2");
                    break;
                case Keycode.Numpad3:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "3");
                    break;
                case Keycode.Numpad4:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "4");
                    break;
                case Keycode.Numpad5:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "5");
                    break;
                case Keycode.Numpad6:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "6");
                    break;
                case Keycode.Numpad7:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "7");
                    break;
                case Keycode.Numpad8:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "8");
                    break;
                case Keycode.Numpad9:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "9");
                    break;
                case Keycode.O:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "O");
                    break;
                case Keycode.P:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "P");
                    break;
                case Keycode.Q:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "Q");
                    break;
                case Keycode.R:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "R");
                    break;
                case Keycode.S:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "S");
                    break;
                case Keycode.Search:
                    MessagingCenter.Send<object>(this, Constants.SearchKey);
                    break;
                case Keycode.Settings:
                    MessagingCenter.Send<object>(this, Constants.SettingsKey);
                    break;
                case Keycode.T:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "T");
                    break;
                case Keycode.U:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "U");
                    break;
                case Keycode.V:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "V");
                    break;
                case Keycode.W:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "W");
                    break;
                case Keycode.X:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "X");
                    break;
                case Keycode.Y:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "Y");
                    break;
                case Keycode.Z:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, "Z");
                    break;
                case Keycode.Space:
                    MessagingCenter.Send<object, string>(this, Constants.KeyEntered, " ");
                    break;
                case Keycode.Del:
                    MessagingCenter.Send<object>(this, Constants.DelKey);
                    break;
                default:
                    //var toast = Toast.MakeText(Forms.Context, "KeyPressed", ToastLength.Short);
                    //toast.SetText(e.KeyCode.ToString());
                    //toast.Show();
                    break;
            }
            return base.OnKeyDown(keyCode, e); ;
        }


    }
}