using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Afaq.IPTV.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<SignInResult> GetRequestAsync(string username, string password)
        {
            var signInResult = new SignInResult();
            try
            {
                using (var client = new HttpClient())
                {

                    using (
                        var response =
                            await
                                client.GetAsync(
                                    new Uri(
                                        $"http://Giant-iptv.com:5477/get.php?username={username}&password={password}&type=m3u_plus&output=mpegts"))
                        )
                    {
                        var content = response.Content;
                        using (content)
                        {

                             signInResult.Channels = await content.ReadAsStringAsync();

                            if (string.IsNullOrEmpty(signInResult.Channels))
                            {
                                signInResult.LoginStatus = LoginStatus.Error;
                                signInResult.StatusMessage = "Wrong username or password";
                            }
                            else
                            {
                                signInResult.LoginStatus = LoginStatus.Successful;
                            }
                        }
                    }
                }
                return signInResult;
            }
            catch (WebException ex)
            {
                switch (ex.Status)
                {
                    case WebExceptionStatus.ConnectFailure:
                        signInResult.LoginStatus = LoginStatus.Error;
                        signInResult.StatusMessage = "Connection Failure";
                        break;
                    case WebExceptionStatus.MessageLengthLimitExceeded:
                        break;
                    case WebExceptionStatus.Pending:
                        break;
                    case WebExceptionStatus.RequestCanceled:
                        signInResult.LoginStatus = LoginStatus.Error;
                        signInResult.StatusMessage = "Login Cancelled";
                        break;
                    case WebExceptionStatus.SendFailure:
                        signInResult.LoginStatus = LoginStatus.Error;
                        signInResult.StatusMessage = "Login Failed";
                        break;
                    case WebExceptionStatus.UnknownError:
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
 
  
                return signInResult;
            }
            catch (HttpRequestException)
            {
                signInResult.LoginStatus = LoginStatus.Error;
                signInResult.StatusMessage = "Could not reach the server";
                return signInResult;
            }
        }
    }

    public enum LoginStatus
    {
        Successful,
        Error
    }

    public class SignInResult
    {
        public LoginStatus LoginStatus { get; set; }
        public string Channels { get; set; }
        public string StatusMessage { get; set; }
    }
}