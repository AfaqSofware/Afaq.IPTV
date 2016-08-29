using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Afaq.IPTV.Helpers;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private const string STR_IS_AUTO_LOGIN = "IsAutoLogin";
        private const string CHANNELS = "channels";
        private const string IS_REMEMBER_ME = "IsRememberMe";
        private readonly IAuthenticationService _authenticationService;
        private readonly IChannelService _channelService;
        private readonly IDbService _dbService;
        private readonly INavigationService _navigationService;
        private bool _canLoginUsernamePassword;
        private bool _isLoginButtonEnabled;
        private bool _isSigningIn;
        private string _password;
        private string _statusMessage;
        private string _username;
        private ObservableCollection<ActivationCode> _activationCodes;



        public LoginPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService, IDbService dbService, IChannelService channelService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            _dbService = dbService;
            _channelService = channelService;
            LoginUsernamePasswordCommand = new DelegateCommand(DoLoginUsernamePassword, () => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && CanLoginUsernamePassword);
            LoginActivationCodesCommand = new DelegateCommand(DoLoginActivationCodes, CanLoginActivationCodes);
            ClearHistoryCommand = new DelegateCommand(DoClearHistory);
           
            var user = _dbService.GetLastSignedInUser();
            Username = user.Username;
            Password = user.Password;
        }

        private bool CanLoginActivationCodes()
        {
            return ActivationCodes.Any(activationCode => activationCode.IsActive);
        }

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
                LoginUsernamePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                LoginUsernamePasswordCommand.RaiseCanExecuteChanged();
            }
        }
        public ObservableCollection<ActivationCode> ActivationCodes
        {

            get { return new ObservableCollection<ActivationCode>(_dbService.GetActivationCodes("Default"));  }
            set
            {
                SetProperty(ref _activationCodes, value);
                LoginActivationCodesCommand.RaiseCanExecuteChanged();
            }
        }

        public bool CanLoginUsernamePassword
        {
            get { return _canLoginUsernamePassword; }
            set
            {
                _canLoginUsernamePassword = value;
                LoginUsernamePasswordCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsRememberPassword
        {
            get
            {
                return Settings.IsRememberPassword;
            }
            set
            {
                Settings.IsRememberPassword = value;
                OnPropertyChanged();
            }
        }

        public bool IsAutoLogin
        {
            get
            {

                return Settings.IsAutoLogin;
            }
            set
            {
                Settings.IsAutoLogin= value;
                OnPropertyChanged();
            }
        }

        public bool IsLoginButtonEnabled
        {
            get { return _isLoginButtonEnabled; }
            set
            {
                _isLoginButtonEnabled = value;
                OnPropertyChanged();
            }
        }

        public bool IsSigningIn
        {
            get { return _isSigningIn; }
            set
            {
                _isSigningIn = value;
                OnPropertyChanged();
            }
        }

        public string StatusMessage
        {
            get { return _statusMessage; }
            set
            {
                _statusMessage = value;
                OnPropertyChanged();
            }
        }

        public DelegateCommand LoginUsernamePasswordCommand { get; set; }
        public DelegateCommand LoginActivationCodesCommand { get; set; }
        public DelegateCommand ClearHistoryCommand { get; set; }


        private async void DoLoginActivationCodes()
        {
            IsSigningIn = true;

            try
            {
                var loginResult = await _authenticationService.GetRequestAsync(ActivationCodes) ;
                var successfulResults = loginResult.Where(a => a.LoginStatus == LoginStatus.Successful).ToList();
                var failureResults = loginResult.Where(a => a.LoginStatus == LoginStatus.Error).ToList();
                var channelsStringLists = new List<string>();
                if (successfulResults.Any())
                {
                    foreach (var successfulResult in successfulResults)
                    {
                        channelsStringLists.Add(successfulResult.Channels);
                    }

                    var channelList = await _channelService.GetAllChannelsAsync(channelsStringLists);
                    var parameters = new NavigationParameters {{CHANNELS, channelList}};

                    await
                        _navigationService.NavigateAsync(new Uri("http://www.Afaq.com/MainPage", UriKind.Absolute),
                            parameters, true);
                }
                else
                {
                    if (failureResults.Any())
                    {
                        StatusMessage = failureResults.First().StatusMessage;
                    }
                }
                IsSigningIn = false;
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessagingCenter.Send<object>(this, Constants.LoginError);
            }
        }

        private async void DoLoginUsernamePassword()
        {
            IsSigningIn = true;

            try
            {
                var loginResult = await _authenticationService.GetRequestAsync(Username, Password);
                var successfulResults = loginResult.Where(a => a.LoginStatus == LoginStatus.Successful).ToList();
                var failureResults = loginResult.Where(a => a.LoginStatus == LoginStatus.Error).ToList();
                var channelsStringLists = new List<string>();
                if (successfulResults.Any())
                {
                    var user = new User
                    {
                        Username = Username,
                        Password = IsRememberPassword ? Password : "",
                        LastSignIn = DateTime.Now
                    };

                    _dbService.SaveUser(user);
                    foreach (var successfulResult in successfulResults)
                    {
                        channelsStringLists.Add(successfulResult.Channels);
                    }

                    var channelList = await _channelService.GetAllChannelsAsync(channelsStringLists);
                    var parameters = new NavigationParameters {{CHANNELS, channelList}};

                    await _navigationService.NavigateAsync(new Uri("http://www.Afaq.com/MainPage", UriKind.Absolute),parameters, true);
                }
                else
                {
                    if (failureResults.Any())
                    {
                        StatusMessage = failureResults.First().StatusMessage;
                    }
                }
                IsSigningIn = false;
                //switch (loginResult[0].LoginStatus)
                //{
                //    case LoginStatus.Successful:
                //        LoginSucceeded?.Invoke(this, null);
                //        var user = new User
                //        {
                //            Username = Username,
                //            Password = IsRememberMe ? Password : "",
                //            LastSignIn = DateTime.Now
                //        };

                //        _dbService.SaveUser(user);
                //        var channels = loginResult[0].Channels;
                //        var channelList = await _channelService.GetAllChannelsAsync(channels); 
                //        var parameters = new NavigationParameters {{CHANNELS, channelList} };

                //        await _navigationService.NavigateAsync(new Uri("http://www.Afaq.com/MainPage", UriKind.Absolute), parameters, true);

                //        break;
                //    case LoginStatus.Error:
                //        StatusMessage = loginResult[0].StatusMessage;
                //        break;
                //    default:
                //        throw new ArgumentOutOfRangeException();
                //}
            }

            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                MessagingCenter.Send<object>(this, Constants.LoginError);
            }
        }

        private void DoClearHistory()
        {
            if (_dbService.ClearUsers())
            {
                Password = "";
                Username = "";
            }
        }
    }
}