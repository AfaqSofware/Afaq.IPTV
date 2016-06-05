using System;
using Afaq.IPTV.Events;
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
        //    _eventAggregator.GetEvent<ExitAppEvent>().Subscribe((a) => this.OnBackPressed());
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
            base.OnKeyDown(keyCode, e);
            switch (e.KeyCode) {
                case Keycode.DpadUp:
                    MessagingCenter.Send<object>(this, "MoveUp");
                    break;

                case Keycode.DpadDown:
                    MessagingCenter.Send<object>(this, "MoveDown");
                    break;

                case Keycode.DpadLeft:
                    MessagingCenter.Send<object>(this, "MoveLeft");
                    break;

                case Keycode.DpadRight:
                    MessagingCenter.Send<object>(this, "MoveRight");
                    break;
                case Keycode.Enter:
                    MessagingCenter.Send<object>(this, "Enter");
                    break;
                case Keycode.Back:
           //         var eventAggregator = _container.Resolve<IEventAggregator>();
             //       eventAggregator.GetEvent<BackButtonPressed>().Publish(new object());
                    break;
                case Keycode.Num0:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "0");
                    break;
                case Keycode.Num1:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "1");
                    break;
                case Keycode.Num2:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "2");
                    break;
                case Keycode.Num3:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "3");
                    break;
                case Keycode.Num4:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "4");
                    break;
                case Keycode.Num5:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "5");
                    break;
                case Keycode.Num6:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "6");
                    break;
                case Keycode.Num7:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "7");
                    break;
                case Keycode.Num8:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "8");
                    break;
                case Keycode.Num9:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "9");
                    break;
                case Keycode.A:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "A");
                    break;
                case Keycode.B:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "B");
                    break;
                case Keycode.C:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "C");
                    break;
                case Keycode.ChannelDown:
                    MessagingCenter.Send<object>(this, "ChannelDown");
                    break;
                case Keycode.ChannelUp:
                    MessagingCenter.Send<object>(this, "ChannelUp");
                    break;
                case Keycode.D:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "D");
                    break;
                case Keycode.E:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "E");
                    break;
                case Keycode.F:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "F");
                    break;
                case Keycode.G:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "G");
                    break;
                case Keycode.H:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "H");
                    break;
                case Keycode.I:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "I");
                    break;
                case Keycode.J:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "J");
                    break;
                case Keycode.K:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "K");
                    break;
                case Keycode.L:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "L");
                    break;
                case Keycode.M:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "M");
                    break;
                case Keycode.Menu:
                    MessagingCenter.Send<object>(this, "MenuKey");
                    break;
                case Keycode.N:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "N");
                    break;
                case Keycode.Numpad0:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "0");
                    break;
                case Keycode.Numpad1:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "1");
                    break;
                case Keycode.Numpad2:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "2");
                    break;
                case Keycode.Numpad3:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "3");
                    break;
                case Keycode.Numpad4:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "4");
                    break;
                case Keycode.Numpad5:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "5");
                    break;
                case Keycode.Numpad6:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "6");
                    break;
                case Keycode.Numpad7:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "7");
                    break;
                case Keycode.Numpad8:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "8");
                    break;
                case Keycode.Numpad9:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "9");
                    break;
                case Keycode.O:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "O");
                    break;
                case Keycode.P:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "P");
                    break;
                case Keycode.Q:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "Q");
                    break;
                case Keycode.R:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "R");
                    break;
                case Keycode.S:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "S");
                    break;
                case Keycode.Search:
                    MessagingCenter.Send<object>(this, "SearchKey");
                    break;
                case Keycode.Settings:
                    MessagingCenter.Send<object>(this, "SettingsKey");
                    break;
                case Keycode.T:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "T");
                    break;
                case Keycode.U:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "U");
                    break;
                case Keycode.V:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "V");
                    break;
                case Keycode.W:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "W");
                    break;
                case Keycode.X:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "X");
                    break;
                case Keycode.Y:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "Y");
                    break;
                case Keycode.Z:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", "Z");
                    break;
                case Keycode.Space:
                    MessagingCenter.Send<object, string>(this, "KeyEntered", " ");
                    break;
                case Keycode.Del:
                    MessagingCenter.Send<object>(this, "DelKey");
                    break;
                default:
                    var toast = Toast.MakeText(Forms.Context, "KeyPressed", ToastLength.Short);
                    toast.SetText(e.KeyCode.ToString());
                    toast.Show();
                    break;
            }
            return true;
        }


    }
}