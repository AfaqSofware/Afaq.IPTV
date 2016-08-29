using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Afaq.IPTV.Models
{
    public class User
    {
        public User()
        {
            Username = string.Empty;
            Password = string.Empty;
            ActivationCodes = new ObservableCollection<ActivationCode>();
            LastSignIn = new DateTimeOffset();
        }
        public string Username { get; set; }

        public string Password { get; set; }

        public ObservableCollection<ActivationCode> ActivationCodes { get; set; }

        public DateTimeOffset LastSignIn { get; set; }        

    }
}
