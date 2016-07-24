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
            if (_viewModel == null)
            {
                return; 
            }

            MessagingCenter.Subscribe<object>(this, Constants.MoveUp, o => { _viewModel.MoveSelectionUp(); });
            MessagingCenter.Subscribe<object>(this, Constants.MoveDown, o => { _viewModel.MoveSelectionDown(); });
            MessagingCenter.Subscribe<object>(this, Constants.MoveLeft, OnMoveLeft);
            MessagingCenter.Subscribe<object>(this, Constants.MoveRight, OnMoveRight);
            MessagingCenter.Subscribe<object>(this, Constants.EnterKey, o => { _viewModel.PlayCurrentChannelCommand.Execute(null); });
            MessagingCenter.Subscribe<object>(this, Constants.DelKey, OnDelete);
            MessagingCenter.Subscribe<object, string>(this, Constants.KeyEntered, OnKeyEntered);
        }

        private void OnMoveRight(object o)
        {
            _viewModel.GetNextChannelListCommand.Execute(null);
            foreach (Channel channel in MyChannelList.ItemsSource) {
                if (channel == _viewModel.CurrentChannel) {
                    MyChannelList.ScrollTo(_viewModel.CurrentChannel, ScrollToPosition.Center, false);
                    break;
                }
            }
        }

        private void OnMoveLeft(object o)
        {
            _viewModel.GetPreviousChannelListCommand.Execute(null);
            foreach (Channel channel in MyChannelList.ItemsSource) {
                if (channel == _viewModel.CurrentChannel) {
                    MyChannelList.ScrollTo(_viewModel.CurrentChannel, ScrollToPosition.Center, false);
                    break;
                }
            }
        }

        private void OnKeyEntered(object o, string key)
        {
            _viewModel.CurrentChannelList.SearchKey += key;
            foreach (Channel channel in MyChannelList.ItemsSource)
            {
                if (channel == _viewModel.CurrentChannel)
                {
                    MyChannelList.ScrollTo(_viewModel.CurrentChannel, ScrollToPosition.Center, false);
                    break; 
                }
            }
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
                if (channel == _viewModel.CurrentChannel)
                {
                    MyChannelList.ScrollTo(_viewModel.CurrentChannel, ScrollToPosition.Center, false);
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