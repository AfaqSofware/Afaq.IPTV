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
                                        $"http://Giant-iptv.com:5477/get.php?username={username}&password={password}&type=m3u&output=mpegts"))
                        )
                    {
                        var content = response.Content;
                        using (content)
                        {

                             signInResult.Channels = await content.ReadAsStringAsync();

                          //  signInResult.Channels = "[{\"Name\":\"All\",\"Channels\":[{\"Name\":\"MBC1\",\"Id\":\"1\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/71.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/x90jsg38j/MBC_1.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/71.ts\",\"Quality\":1}},{\"Name\":\"MBC2\",\"Id\":\"2\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/72.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/5mxs7rjvn/MBC_2.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/72.ts\",\"Quality\":1}},{\"Name\":\"Bein Sport 1\",\"Id\":\"3\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/1.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/4ylb1d65f/Bein_Sport1.jpg\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/1.ts\",\"Quality\":1}},{\"Name\":\"Bein Sport 2\",\"Id\":\"4\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/2.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/4ahifjdr7/Bein_Sport2.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/2.ts\",\"Quality\":1}},{\"Name\":\"Bein Sport 3\",\"Id\":\"5\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/3.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/ovwa7fvc3/Bein_Sport3.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/3.ts\",\"Quality\":1}},{\"Name\":\"Aljazeera News - Arabic\",\"Id\":\"6\",\"Sources\":[{\"VideoSource\":\"http://sat.010e.net:8000/live/ahmedmadi/123456/215.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/497km4bxf/Aljazeera_News.png\",\"CurrentSource\":{\"VideoSource\":\"http://sat.010e.net:8000/live/ahmedmadi/123456/215.ts\",\"Quality\":1}}]},{\"Name\":\"Sport\",\"Channels\":[{\"Name\":\"Bein Sport 1\",\"Id\":\"3\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/1.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/4ylb1d65f/Bein_Sport1.jpg\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/1.ts\",\"Quality\":1}},{\"Name\":\"Bein Sport 2\",\"Id\":\"4\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/2.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/4ahifjdr7/Bein_Sport2.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/2.ts\",\"Quality\":1}},{\"Name\":\"Bein Sport 3\",\"Id\":\"5\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/3.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/ovwa7fvc3/Bein_Sport3.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/3.ts\",\"Quality\":1}}]},{\"Name\":\"MBC\",\"Channels\":[{\"Name\":\"MBC1\",\"Id\":\"1\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/71.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/x90jsg38j/MBC_1.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/71.ts\",\"Quality\":1}},{\"Name\":\"MBC2\",\"Id\":\"2\",\"Sources\":[{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/72.ts\",\"Quality\":1}],\"Logo\":\"http://s19.postimg.org/5mxs7rjvn/MBC_2.png\",\"CurrentSource\":{\"VideoSource\":\"http://star.010e.net:8000/live/ahmedmadi/123456/72.ts\",\"Quality\":1}}]}]";
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