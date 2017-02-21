using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Helper;
using LIMMA.Interfaces;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace LIMMA.Services
{
    public class ConnectionService : IConnectionServices
    {
        public UserToken UserToken { get; set; }
        public async Task<string> GetConnection(IConfiguration config)
        {
            //var response = await Helper.UserToken.UpdateToken(config);

            


            return "";
        }

        public async Task<UserToken> GetUserToken(IConfiguration config)
        {


            var response = await Helper.UserToken.GetToken(config);

            return response;
        }

        public async Task<UserToken> UpdateUserToken(IConfiguration config, UserToken token)
        {
            throw new NotImplementedException();
        }

        public ConnectionService()
        {
            //UserToken = GetCurrentToken(config).Result;
            UserToken = new UserToken();
        }

        public async Task<UserToken> GetCurrentToken(IConfiguration config)
        {
            Debug.WriteLine("GetCurrentToken - Started");
            UserToken token = new UserToken();
            if (Application.Current.Properties.ContainsKey("Token"))
            {
                Debug.WriteLine("GetCurrentToken - LocalToken Found");

                dynamic storedToken = JsonConvert.DeserializeObject(Application.Current.Properties["Token"].ToString());
                token.Token = storedToken.Token;
                token.Expires = storedToken.Expires;
                token.Success = storedToken.Success;
                token.TokenPrefix = storedToken.TokenPrefix;

                var s = token.Expires;

                var date = s.Split(' ')[0];
                var year = date.Split('/')[2];
                var month = date.Split('/')[0];
                var day = date.Split('/')[1];
                var time = s.Split(' ')[1];
                var dt = $"{year}/{month}/{day} {time}";

                if (DateTime.Parse(dt) >= DateTime.Now)
                {
                    Debug.WriteLine("GetCurrentToken - Updating Token");
                    token =
                        await
                            UpdateUserToken(
                                config, token);
                    this.UserToken = token;
                    Application.Current.Properties["Token"] = JsonConvert.SerializeObject(token);
                    return token;
                }

            }
            Debug.WriteLine("GetCurrentToken - Requesting new Token");
            token = await GetUserToken(config);
            Application.Current.Properties["Token"] = JsonConvert.SerializeObject(token);
            this.UserToken = token;
            return token;

        }

    }
}
