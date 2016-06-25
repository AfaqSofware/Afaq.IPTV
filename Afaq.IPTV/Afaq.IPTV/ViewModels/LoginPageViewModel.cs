using System;
using System.Linq;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Realms;
using Xamarin.Forms;

namespace Afaq.IPTV.ViewModels
{

    public class LoginPageViewModel : BindableBase
    {
        private const string StrChannels = "channels";
        private readonly IAuthenticationService _authenticationService;
        private readonly INavigationService _navigationService;

        private readonly Realm _realm;
        private bool _isAutoLogin;
        private bool _isLoginButtonEnabled;
        private bool _isRememberMe;
        private bool _isSigningIn;
        private string _password;
        private string _statusMessage;
        private string _username;
        private bool _canLogin;

        public event EventHandler LoginSucceeded;
        public LoginPageViewModel(INavigationService navigationService, IAuthenticationService authenticationService)
        {
            _navigationService = navigationService;
            _authenticationService = authenticationService;
            LoginCommand = new DelegateCommand(Login, () => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password) && CanLogin);
            _realm = Realm.GetInstance();
            if (!_realm.All<Credentials>().ToList().Any()) return;
            var credentials = _realm.All<Credentials>().ToList().First();
            if (credentials == null) return;

            Username = credentials.Username;
            Password = credentials.Password;
            IsRememberMe = credentials.IsRememberMe;
            IsAutoLogin = credentials.IsAutoLogin;
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

        public bool CanLogin
        {
            get
            {
                return _canLogin;
            }
            set
            {
                _canLogin = value;
                LoginCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsRememberMe
        {
            get { return _isRememberMe; }
            set
            {
                _isRememberMe = value;
                OnPropertyChanged();
            }
        }

        public bool IsAutoLogin
        {
            get { return _isAutoLogin; }
            set
            {
                _isAutoLogin = value;
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

        public DelegateCommand LoginCommand { get; set; }

        private async void Login()
        {
            IsSigningIn = true;

            var loginResult = await _authenticationService.GetRequestAsync(Username, Password);
            IsSigningIn = false;
            switch (loginResult.LoginStatus) {
                case LoginStatus.Successful:
                    LoginSucceeded?.Invoke(this, null);
                    var usrname = Username;
                    var credentials = _realm.All<Credentials>().Where(d => d.Username == usrname).ToList();

                    foreach (var credential in credentials) {
                        using (var trans = _realm.BeginWrite()) {
                            _realm.Remove(credential);
                            trans.Commit();
                        }
                    }

                    _realm.Write(() =>
                    {
                        var entry = _realm.CreateObject<Credentials>();

                        if (IsRememberMe) {
                            entry.Password = Password;
                        }
                        entry.Username = Username;
                        entry.IsAutoLogin = IsAutoLogin;
                        entry.IsRememberMe = IsRememberMe;
                    });


                    var channels = loginResult.Channels;
                    var parameters = new NavigationParameters { { StrChannels, channels } };
          
                    await _navigationService.NavigateAsync(new Uri("http://www.Afaq.com/MainPage", UriKind.Absolute), parameters, true);

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