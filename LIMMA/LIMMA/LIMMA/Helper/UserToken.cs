using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace LIMMA.Helper
{
    public class UserToken
    {
        public bool Success { get; set; }
        public string Token { get; set; }
        public string Expires { get; set; }
        public string TokenPrefix { get; set; }

        public void AddAuthorizationHeader(WebRequest request)
        {
            //request.Headers.Add("Authorization", TokenPrefix + Token);
        }




        public static async Task<String> UpdateToken(IConfiguration config)
        {
            //var authData = $"{config.User}:{config.Password}";
            //var authHeaderValue = Convert.ToBase64String(Encoding.UTF8.GetBytes(authData));
            

            //HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            //client.BaseAddress = new Uri(config.BaseUrl);

            //var response = await client.PostAsync(config.BaseUrl + "/api/cms/membership-user/token", null);
            
            //return response.RequestMessage.Content.ToString();

            return "";



            //var request = (HttpWebRequest)WebRequest.Create(config.BaseUrl + "/api/cms/membership-user/token");
            //request.Method = "POST";
            //request.ContentType = "application/json";
            //string serializeObject =
            //    _serializer.Serialize(new { Email = config.User, Password = config.Password });
            //string result = _webApiRequestExecutor.ExecuteRequest(request, serializeObject, null);
            //return _serializer.Deserialize<UserToken>(result);

        }

        public static async Task<UserToken> GetToken(IConfiguration config)
        {
            
            HttpClient client = new HttpClient();
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", authHeaderValue);
            client.BaseAddress = new Uri(config.BaseUrl);

            var auth = new
            {
                EMail = config.User,
                Password = config.Password
            };


            string body = JsonConvert.SerializeObject(auth);
            HttpContent bodyContent = new StringContent(body);
            bodyContent.Headers.ContentType = MediaTypeHeaderValue.Parse("application/json");
            var response = await client.PostAsync(config.BaseUrl + "/api/cms/membership-user/token", bodyContent);

            if (response.IsSuccessStatusCode)
            {
                var readableResponse = await ((StreamContent) response.Content).ReadAsStringAsync();
                dynamic token = JsonConvert.DeserializeObject(readableResponse);
                UserToken utoken = new UserToken();
                utoken.Success = token.Success;
                utoken.Token = token.Token;
                utoken.Expires = token.Expires;
                utoken.TokenPrefix = token.TokenPrefix;
                return utoken;
            }
            else
            {
                throw new HttpRequestException("Unable to retrieve Token");
            }



            //var request = (HttpWebRequest)WebRequest.Create(config.BaseUrl + "/api/cms/membership-user/token");
            //request.Method = "POST";
            //request.ContentType = "application/json";
            //string serializeObject =
            //    _serializer.Serialize(new { Email = config.User, Password = config.Password });
            //string result = _webApiRequestExecutor.ExecuteRequest(request, serializeObject, null);
            //return _serializer.Deserialize<UserToken>(result);

        }
    }
}
