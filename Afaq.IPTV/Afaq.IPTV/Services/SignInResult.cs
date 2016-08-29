namespace Afaq.IPTV.Services
{
    public class SignInResult
    {
        public LoginStatus LoginStatus { get; set; }
        public string Channels { get; set; }
        public string StatusMessage { get; set; }
    }
}