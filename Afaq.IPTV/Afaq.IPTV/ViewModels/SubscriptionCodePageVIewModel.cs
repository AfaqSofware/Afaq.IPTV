using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Commands;
using Prism.Mvvm;

namespace Afaq.IPTV.ViewModels
{
    public class SubscriptionCodePageVIewModel:BindableBase
    {
        private string _subscriptionCode;

        public SubscriptionCodePageVIewModel()
        {
              AddSubscriptionCodeCommand = new DelegateCommand(AddSubscription,HasSubscriptionCodeValue);  
        }
        public string SubscriptionCode
        {
            get { return _subscriptionCode; }
            set
            {
                SetProperty(ref _subscriptionCode, value); 
                AddSubscriptionCodeCommand.RaiseCanExecuteChanged();
            }
        }



        public DelegateCommand AddSubscriptionCodeCommand { get; set; }

        private async void AddSubscription()
        {
            
        }

        private bool HasSubscriptionCodeValue()
        {
            return string.IsNullOrEmpty(_subscriptionCode); 
        }

    }
}
