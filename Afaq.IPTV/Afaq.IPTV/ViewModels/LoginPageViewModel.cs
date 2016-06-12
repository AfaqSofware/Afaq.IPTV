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

        private readonly Realm _realm;
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
            LoginCommand = new DelegateCommand(Login,
                () => !string.IsNullOrEmpty(Username) && !string.IsNullOrEmpty(Password));


            _realm = Realm.GetInstance();

            if (!_realm.All<Credentials>().ToList().Any()) return;
            var credentials = _realm.All<Credentials>().ToList().First();
            if (credentials == null) return;

            Password = credentials.Password;
            Username = credentials.Username;
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


        private async void Login()
        {
            IsSigningIn = true;
            var loginResult = await _authenticationService.GetRequestAsync(Username, Password);
            IsSigningIn = false;
            switch (loginResult.LoginStatus)
            {
                case LoginStatus.Successful:
                    var usrname = Username;
                    var credentials = _realm.All<Credentials>().Where(d => d.Username == usrname).ToList();
      
                    foreach (var credential in credentials)
                    {
                        using (var trans = _realm.BeginWrite()) {
                            _realm.Remove(credential);
                            trans.Commit();
                        }
                    }
                
                    _realm.Write(() =>
                    {
                        var entry = _realm.CreateObject<Credentials>();
                        entry.Password = Password;
                        entry.Username = Username;
                    });


                    var channels = loginResult.Channels;
                    var parameters = new NavigationParameters {{StrChannels, channels}};
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