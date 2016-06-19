using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Realms;

namespace Afaq.IPTV.Models
{
    public class Credentials:RealmObject
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public bool IsAutoLogin { get; set; }
        public bool IsRememberMe { get; set; }
    }
}
