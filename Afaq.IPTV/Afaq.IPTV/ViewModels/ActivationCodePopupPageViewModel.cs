using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Afaq.IPTV.Models;
using Afaq.IPTV.Services;
using Prism.Commands;
using Prism.Mvvm;
using Xamarin.Forms;

namespace Afaq.IPTV.ViewModels
{
    public class ActivationCodePopupPageViewModel:BindableBase
    {
        private readonly IAuthenticationService _authenticationService;
        private readonly IDbService _dbService;
        private string _activationCode;
        private string _userName; 
        private bool _isActivationCodeActive;
        private ObservableCollection<ActivationCode> _activationCodes;
        private string _status;
        private Color _statusColor;

        public ActivationCodePopupPageViewModel(IAuthenticationService authenticationService, IDbService dbService)
        {
            _authenticationService = authenticationService;
            _dbService = dbService;
            AddSubscriptionCodeCommand = new DelegateCommand(AddSubscriptionCode,HasSubscriptionCodeValue);
            ActivationCodes = new ObservableCollection<ActivationCode>();
            _dbService.ActivationCodesModified += OnActivationCodesModified; 


        }

        private void OnActivationCodesModified(object sender, EventArgs e)
        {
          Init(_userName);
        }

        public ObservableCollection<ActivationCode> ActivationCodes
        {
            get { return _activationCodes; }
            set { SetProperty(ref _activationCodes, value); }
        }

        public string ActivationCode
        {
            get { return _activationCode; }
            set
            {
                SetProperty(ref _activationCode, value); 
                AddSubscriptionCodeCommand.RaiseCanExecuteChanged();
            }
        }

        public string Status
        {
            get { return _status; }
            set { SetProperty(ref _status, value); }
        }

        public Color StatusColor
        {
            get { return _statusColor; }
            set { SetProperty(ref _statusColor , value) ; }
        }

        public bool IsActivationCodeActive
        {
            get { return _isActivationCodeActive; }
            set { SetProperty(ref _isActivationCodeActive , value); }
        }


        public DelegateCommand AddSubscriptionCodeCommand { get; set; }

        public void Init(string userName)
        {
            _userName = userName; 
            ActivationCodes = new ObservableCollection<ActivationCode>(_dbService.GetActivationCodes(_userName));
        }

        private async void AddSubscriptionCode()
        {
            var activationCode = new ActivationCode()
            {
                Id = ActivationCode,
                IsActive = true
            };
            if (await IsActivationCodeValidAsync(activationCode)) {
                activationCode.SetDatabaseService(_dbService);
                ActivationCodes.Add(activationCode);
                _dbService.SaveActivationCode(_userName, activationCode);
                Status = "Activation code added susccessfully";
                StatusColor = Color.Green;
            }
            else
            {
                Status = "Activation code could not be added";
                StatusColor = Color.Red;
            }
      
        }

        private async Task<bool> IsActivationCodeValidAsync(ActivationCode activationCode)
        {
            var activationCodeValidationResult = await _authenticationService.GetRequestAsync(new List<ActivationCode>() { activationCode });
            return activationCodeValidationResult[0].LoginStatus == LoginStatus.Successful;
        }

        private bool HasSubscriptionCodeValue()
        {           
            return !string.IsNullOrEmpty(_activationCode);  
        }
    }
}
