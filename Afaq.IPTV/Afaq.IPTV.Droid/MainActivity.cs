using Afaq.IPTV.Events;
using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Google.Android.Exoplayer;
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
        private ScreenOrientation _lastOrientation;


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
            _lastOrientation = RequestedOrientation;
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
     
        //public override bool OnKeyDown(Keycode keyCode, KeyEvent e)
        //{
           
        //    switch (e.KeyCode)
        //    {
        //        case Keycode.DpadUp:
        //            MessagingCenter.Send<object>(this, "MoveUp");
        //            break;
        //        case Keycode.DpadDown:
        //            MessagingCenter.Send<object>(this, "MoveDown");
        //            break;
        //        case Keycode.Enter:
        //            MessagingCenter.Send<object>(this, "Enter");
        //            break;
        //        case Keycode.Back:
        //            var eventAggregator = _container.Resolve<IEventAggregator>();
        //            eventAggregator.GetEvent<BackButtonPressed>().Publish(new object());
        //            break;
        //    }
        //    return true;
        //}
    }
}