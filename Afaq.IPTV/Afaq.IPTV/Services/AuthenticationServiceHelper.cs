using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Afaq.IPTV.Services
{
    public static class AuthenticationServiceHelper
    {
        public static async Task<SignInResult> GetSignInResultAsync(string server, string username, string password)
        {
            var signInResult = new SignInResult();
            try {
                using (var client = new HttpClient()) {
                    using (
                        var response =
                            await
                                client.GetAsync(
                                    new Uri(
                                        $"http://{server}/get.php?username={username}&password={password}&type=m3u_plus&output=mpegts"))
                        ) {
                        var content = response.Content;
                        using (content) {
                            signInResult.Channels = await content.ReadAsStringAsync();

                            if (string.IsNullOrEmpty(signInResult.Channels)) {
                                signInResult.LoginStatus = LoginStatus.Error;
                                signInResult.StatusMessage = "Wrong username or password";
                            } else {
                                signInResult.LoginStatus = LoginStatus.Successful;
                            }
                        }
                    }
                }
            }
            catch (WebException ex) {
                switch (ex.Status) {
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
                        signInResult.LoginStatus = LoginStatus.Error;
                        signInResult.StatusMessage = "Connection Failure";
                        break;
                }
            }
            catch (HttpRequestException) {
                signInResult.LoginStatus = LoginStatus.Error;
                signInResult.StatusMessage = "Could not reach the server";
            }
            return signInResult;
        }
    }
}
