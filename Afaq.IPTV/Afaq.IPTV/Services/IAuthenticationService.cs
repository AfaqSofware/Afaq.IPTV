using System.Threading.Tasks;

namespace Afaq.IPTV.Services
{
    public interface IAuthenticationService
    {
        Task<SignInResult> GetRequestAsync(string username, string password);
    }
}