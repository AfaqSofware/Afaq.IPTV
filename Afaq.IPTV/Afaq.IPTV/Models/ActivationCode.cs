using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Afaq.IPTV.Services;
using Xamarin.Forms;

namespace Afaq.IPTV.Models
{
    public class ActivationCode
    {
        private IDbService _dbService;
        private readonly string _userName;

        public ActivationCode( string userName ="Default")
        {
            _userName = userName;
            DeleteCommand = new Command(DoDeleteCommand);
        }

        private bool _isActive;
        public string Id { get; set; }

        public bool IsActive
        {
            get { return _isActive; }
            set
            {
                _isActive = value;
                _dbService?.SaveActivationCode(_userName, this);
            }
        }

        public void SetDatabaseService(IDbService dbService)
        {
            _dbService = dbService; 
        }

        public Command DeleteCommand { get; set; }

        private void DoDeleteCommand ()
        {
            _dbService.RemoveActivationCode(_userName, this);
            
        }
    }
}
