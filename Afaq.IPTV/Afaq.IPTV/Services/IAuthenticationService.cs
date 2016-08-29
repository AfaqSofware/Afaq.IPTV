using System.Collections.Generic;
using System.Threading.Tasks;
using Afaq.IPTV.Models;

namespace Afaq.IPTV.Services
{
    public interface IAuthenticationService
    {
        Task<List<SignInResult>> GetRequestAsync(string username, string password);
        Task<List<SignInResult>> GetRequestAsync(IList<ActivationCode> activationCodes );
    }
}