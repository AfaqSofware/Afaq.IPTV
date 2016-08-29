using Afaq.IPTV.Helpers;
using Afaq.IPTV.Models;
using Afaq.IPTV.ViewModels;
using Xamarin.Forms;

namespace Afaq.IPTV.Views
{
    public partial class TvMainPage
    {
        private readonly IMainPageViewModel _viewModel;

        public TvMainPage(IMainPageViewModel viewModel)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel; 
        }

        private void OnMoveRight(object o)
        {
            _viewModel.GetNextChannelListCommand.Execute(null);
            foreach (Channel channel in MyChannelList.ItemsSource) {
                if (channel == MyChannelList.SelectedItem) {
                    MyChannelList.ScrollTo(MyChannelList.SelectedItem, ScrollToPosition.Center, false);
                    break;
                }
            }
        }

        private void OnMoveLeft(object o)
        {
            _viewModel.GetPreviousChannelListCommand.Execute(null);
            foreach (Channel channel in MyChannelList.ItemsSource) {
                if (channel == MyChannelList.SelectedItem) {
                    MyChannelList.ScrollTo(MyChannelList.SelectedItem, ScrollToPosition.Center, false);
                    break;
                }
            }
        }

        private void OnKeyEntered(object o, string key)
        {
            _viewModel.CurrentChannelList.SearchKey += key;
            foreach (Channel channel in MyChannelList.ItemsSource)
            {
                if (channel == MyChannelList.SelectedItem)
                {
                    MyChannelList.ScrollTo(MyChannelList.SelectedItem, ScrollToPosition.Center, false);
                    break; 
                }
            }
        }


        protected override void OnDisappearing()
        {
            MessagingCenter.Send<object>(this, Constants.HidePlayer);


            MessagingCenter.Unsubscribe<object>(this, Constants.MoveUp);
            MessagingCenter.Unsubscribe<object>(this, Constants.MoveDown);
            MessagingCenter.Unsubscribe<object>(this, Constants.MoveLeft);
            MessagingCenter.Unsubscribe<object>(this, Constants.MoveRight);
            MessagingCenter.Unsubscribe<object>(this, Constants.EnterKey);
            MessagingCenter.Unsubscribe<object>(this, Constants.DelKey);
            MessagingCenter.Unsubscribe<object, string>(this, Constants.KeyEntered);
            MessagingCenter.Unsubscribe<object>(this, Constants.AppPaused);
            MessagingCenter.Unsubscribe<object>(this, Constants.AppResumed);
            base.OnDisappearing();
        }

        protected override void OnAppearing()
        {
            _viewModel.CanPlay = true;
            MessagingCenter.Send<object>(this, Constants.ShowPlayer);

            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, OnMoveUp);
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, OnMoveDown);
            MessagingCenter.Subscribe<object>(this, Constants.MoveLeft, OnMoveLeft);
            MessagingCenter.Subscribe<object>(this, Constants.MoveRight, OnMoveRight);
            MessagingCenter.Subscribe<object>(this, Constants.EnterKey, o => { _viewModel.PlayCurrentChannelCommand.Execute(MyChannelList.SelectedItem); });
            MessagingCenter.Subscribe<object>(this, Constants.DelKey, OnDelete);
            MessagingCenter.Subscribe<object, string>(this, Constants.KeyEntered, OnKeyEntered);
            MessagingCenter.Subscribe<object>(this, Constants.AppPaused, OnAppPaused);
            MessagingCenter.Subscribe<object>(this, Constants.AppResumed, OnAppResumed);


            base.OnAppearing();
        }

        private void OnMoveUp(object o)
        {
            System.Diagnostics.Debug.WriteLine("###Going Up####");
            _viewModel.MoveSelectionUp();
        }

        private void OnMoveDown(object o)
        {
            _viewModel.MoveSelectionDown();
        }

        private void OnAppResumed(object obj)
        {
            MessagingCenter.Send<object>(this, Constants.ShowPlayer);
        }

        private void OnAppPaused(object obj)
        {
            MessagingCenter.Send<object>(this, Constants.StopPlayer);
        }

        private void OnDelete(object obj)
        {
            var oldtext = _viewModel.CurrentChannelList.SearchKey;
            if (!string.IsNullOrEmpty(oldtext))
            {
                var newText = oldtext.Remove(oldtext.Length - 1);
                _viewModel.CurrentChannelList.SearchKey = newText;
            }
            foreach (Channel channel in MyChannelList.ItemsSource) 
            {
                if (channel == MyChannelList.SelectedItem)
                {
                    MyChannelList.ScrollTo(MyChannelList.SelectedItem, ScrollToPosition.Center, false);
                    break;
                }
            }
        }

        protected override bool OnBackButtonPressed()
        {
            _viewModel.SignOut();
            return true;
        }

        private void MyChannelList_OnItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            if (MyChannelList.SelectedItem != null)
                MyChannelList.ScrollTo(MyChannelList.SelectedItem, ScrollToPosition.Center, false);
        }
    }
}