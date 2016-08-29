using System.Collections.Generic;
using System.Threading.Tasks;
using Afaq.IPTV.Models;

namespace Afaq.IPTV.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<List<SignInResult>> GetRequestAsync(string username, string password)
        {
            var signInResults = new List<SignInResult>();
            var signInResult =
                await AuthenticationServiceHelper.GetSignInResultAsync("Giant-iptv.com:5477", username, password);
            signInResults.Add(signInResult);
            return signInResults;
        }

        public async Task<List<SignInResult>> GetRequestAsync(IList<ActivationCode> activationCodes)
        {
            var signInResults = new List<SignInResult>();
            foreach (var activationCode in activationCodes)
            {
                if (!activationCode.IsActive) continue;
                var signInResult =
                    await
                        AuthenticationServiceHelper.GetSignInResultAsync("80.241.214.244:5666", activationCode.Id,"123456");
                signInResults.Add(signInResult);
            }
            return signInResults;
        }
    }
}