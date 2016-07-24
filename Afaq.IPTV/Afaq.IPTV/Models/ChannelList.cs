using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using Afaq.IPTV.Helpers;
using Prism.Mvvm;

namespace Afaq.IPTV.Models
{
    public class ChannelList : BindableBase
    {
        #region private members

        private ObservableCollection<Channel> _channels;
        private Channel _currentChannel;
        private string _name;
        private string _searchKey;
        private readonly ChannelListHelper _channelListHelper;
        private string _currentVideoSource;

        #endregion

        #region Events

        public event EventHandler<Channel> ChannelChanged;

        #endregion

        public ChannelList()
        {
            _channelListHelper = new ChannelListHelper();
            Channels = new ObservableCollection<Channel>();
            BackupChannels = new ObservableCollection<Channel>();
        }

        #region Properties



        public Channel CurrentChannel
        {
            get { return _currentChannel; }
            set
            {
                if (Equals(value, _currentChannel)) return;
                _currentChannel = value;
                CurrentVideoSource = value.CurrentSource.VideoSource;
                OnPropertyChanged();
            }
        }

        public string CurrentVideoSource
        {
            get { return _currentVideoSource; }
            set
            {
                _currentVideoSource = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get { return _name; }
            set
            {
                if (value == _name) return;
                _name = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Channel> Channels
        {
            get { return _channels; }
            set
            {
                if (Equals(value, _channels)) return;
                _channels = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Channel> BackupChannels { get; set; }

        public string SearchKey
        {
            get { return _searchKey; }
            set
            {
                if (value == null) return;
                Channels = _channelListHelper.GetChannels(value, BackupChannels);
                _searchKey = value;
                OnPropertyChanged();
            }
        }

        #endregion

        #region Public methods

        public void MoveSelectionDown(object arg1)
        {
            var index = Channels.IndexOf(CurrentChannel);
            if (index == -1)
            {
                if (Channels.Any())
                {
                    CurrentChannel = Channels.First();
                }
            }
            if (index < Channels.Count - 1)
            {
                CurrentChannel = Channels[index + 1];
            }
        }

        public void MoveSelectionUp(object arg1)
        {
            var index = Channels.IndexOf(CurrentChannel);
            if (index == -1)
            {
                if (Channels.Any())
                {
                    CurrentChannel = Channels.First();
                }
            }
            if (index > 0)
            {
                CurrentChannel = Channels[index - 1];
            }
        }

        #endregion

        public void PlaySelectedChannel()
        {
            ChannelChanged?.Invoke(this, CurrentChannel);
        }
    }
}