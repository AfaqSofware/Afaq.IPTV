using System;
using System.Collections.Generic;
using Realms;

namespace Afaq.IPTV.Services.DO
{
    public class UserDO : RealmObject
    {
        [ObjectId]
        public string UserName { get; set; }
        public string Password { get; set; }
        public RealmList<ActivationCodeDO> ActivationCodes { get; }
        public DateTimeOffset LastSignIn { get; set; }
    }
}