using Realms;

namespace Afaq.IPTV.Services.DO
{
    /// <summary>
    /// Represents the activation code which users activate to get different services  
    /// </summary>
    public class ActivationCodeDO:RealmObject
    {
        [ObjectId]
        public string Id { get; set; }

        public bool IsActive { get; set; }
    }
}
