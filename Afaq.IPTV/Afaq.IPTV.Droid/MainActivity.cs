using Afaq.IPTV.Events;
using Afaq.IPTV.Helpers;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Content.Res;
using Android.Hardware;
using Android.OS;
using Android.Views;
using Android.Widget;
using Microsoft.Practices.Unity;
using Prism.Events;
using Prism.Unity;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Android.Views.View;

namespace Afaq.IPTV.Droid
{
    [Activity(Label = "Afaq.IPTV", Icon = "@drawable/icon", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    [IntentFilter(new[] {Intent.ActionMain}, Categories = new[] {Intent.CategoryLauncher, Intent.CategoryLeanbackLauncher})]
    public class MainActivity : FormsApplicationActivity
    {
        private PrismApplication _application;
        private IUnityContainer _container;
        private IEventAggregator _eventAggregator;
        private bool _isTV; 
        
        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            
            Forms.Init(this, bundle);

            var uiModeManager = (UiModeManager) GetSystemService(UiModeService);
            if (uiModeManager.CurrentModeType == UiMode.TypeTelevision)
            {
                _isTV = true; 
                _application = new TvApp();
            }
            else
            {
                _isTV = false; 
                _application = new MobileApp();
            }

            _container = _application.Container;
            LoadApplication(_application);
            _eventAggregator = _container.Resolve<IEventAggregator>();
            _eventAggregator.GetEvent<CinemaModeEvent>().Subscribe(OnCinemaModeEvent);
            MessagingCenter.Subscribe<object>(this, Constants.LoginError, OnLoginError);
        }

        private void OnLoginError(object obj)
        {
            Toast.MakeText(this,"Login problems. Check your internet connection",ToastLength.Short).Show();
        }


        private void OnCinemaModeEvent(bool cenimaMode)
        {
            if (cenimaMode)
            {
                Window.AddFlags(WindowManagerFlags.Fullscreen);

                Window.AddFlags(WindowManagerFlags.KeepScreenOn);
            }
            else
            {
                Window.ClearFlags(WindowManagerFlags.Fullscreen);
                Window.ClearFlags(WindowManagerFlags.KeepScreenOn);
            }
        }

        protected override void OnPause()
        {
            base.OnPause();
            MessagingCenter.Send<object>(this, "Paused");
        }

        public override bool OnKeyUp(Keycode keyCode, KeyEvent e)
        {
            if (_isTV)
            {
                switch (e.KeyCode) {
                    case Keycode.DpadUp:
                    case Keycode.DpadDown:
                    case Keycode.DpadLeft:
                    case Keycode.DpadRight:
                        return true;
                }
            }
          
            return base.OnKeyUp(keyCode, e);
        }

        public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        {
            if (true)
            {
                switch (e.KeyCode) {
                    case Keycode.DpadUp:
                        MessagingCenter.Send<object>(this, Constants.MoveUp);
                        return true;

                    case Keycode.DpadDown:
                        MessagingCenter.Send<object>(this, Constants.MoveDown);
                        return true;

                    case Keycode.DpadLeft:
                        MessagingCenter.Send<object>(this, Constants.MoveLeft);
                        return true;

                    case Keycode.DpadRight:
                        MessagingCenter.Send<object>(this, Constants.MoveRight);
                        return true;

                    case Keycode.DpadCenter:
                    case Keycode.Enter:
                        MessagingCenter.Send<object>(this, Constants.EnterKey);
                        break;

                    case Keycode.VolumeUp:
                        MessagingCenter.Send<object>(this, Constants.VolumeUp);
                        break;
                    case Keycode.VolumeDown:
                        MessagingCenter.Send<object>(this, Constants.VolumeDown);
                        break;
                    case Keycode.VolumeMute:
                        MessagingCenter.Send<object>(this, Constants.VolumeMute);
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
                }
            }
         
            return base.OnKeyDown(keyCode, e);
        }
    }
}