using System;
using Afaq.IPTV.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;

namespace Afaq.IPTV.ViewModels
{
    public class LoginPageViewModel : BindableBase
    {
        private const string StrPassword = "Password";
        private const string StrUsername = "Username";
        private const string StrChannels = "channels";

        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;
        private bool _isSigningIn;
        private string _password;
        private string _statusMessage;
        private string _username;

        public LoginPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            LoginCommand = new DelegateCommand(Login, CanExecuteLogin);
            Password = "123456";
            Username = "ahmedmadi";
            if (Application.Current.Properties.ContainsKey(StrPassword))
            {
               // Password = (string)Application.Current.Properties[StrPassword];
            }

            if (Application.Current.Properties.ContainsKey(StrUsername)) {
                //Username = (string)Application.Current.Properties[StrUsername];
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

        public string Username
        {
            get { return _username; }
            set
            {
                _username = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged();
                LoginCommand.RaiseCanExecuteChanged();
            }
        }


        public DelegateCommand LoginCommand { get; set; }


        private bool CanExecuteLogin()
        {
            return !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password);
        }

        private async void Login()
        {
            IsSigningIn = true;
            var loginResult = await _authenticationService.GetRequestAsync(Username, Password);
            IsSigningIn = false;
            switch (loginResult.LoginStatus) {
                case LoginStatus.Successful:
                    Application.Current.Properties[StrPassword] = Password;
                    Application.Current.Properties[StrUsername] = Username;
                    var channels = loginResult.Channels;
                    var parameters = new NavigationParameters { { StrChannels, channels } };
                    await _navigationService.NavigateAsync("MainPage", parameters);

                    break;
                case LoginStatus.Error:
                    StatusMessage = loginResult.StatusMessage;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}