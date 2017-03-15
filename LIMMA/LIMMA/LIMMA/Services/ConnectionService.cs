using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Helper;
using LIMMA.Interfaces;
using LIMMA.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
                // Probably there is a nicer way available, but well it works for reformatting
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

        public Task<AppStructure> GetAppStructure(IConfiguration config)
        {
            Debug.WriteLine("GetAppStructure - Started");

            var structure = AppStructure.GetAppStructure(config);

            return structure;


        }

        public async Task<DataModel> GetDataModel(Guid DataSourceID, IConfiguration config)
        {
            Debug.WriteLine("Datamodel Request: " + DataSourceID.ToString());
            UserToken token = new UserToken();
            dynamic savedToken = JsonConvert.DeserializeObject(Application.Current.Properties["Token"].ToString());
            token.Token = savedToken.Token;
            token.Expires = savedToken.Expires;
            token.TokenPrefix = savedToken.TokenPrefix;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token.TokenPrefix + token.Token);
            var url = config.BaseUrl + "api/imma/node/" + DataSourceID + "/data";
            try
            {
                var result =
                await
                    client.PostAsync(url, null);
                var readableResponse = await ((StreamContent)result.Content).ReadAsStringAsync();
                dynamic qwert = JsonConvert.DeserializeObject(readableResponse);
                var asdf = (DataModel)JsonConvert.DeserializeObject(readableResponse, typeof(DataModel));
                //var asdf = (DataModel) qwert.First.First.First;

                return asdf;
            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                Debug.WriteLine("Exception Receiving DataModel:");
                Debug.WriteLine("- Message:" + ex.Message);
                Debug.WriteLine("- HasInner:" + ex.InnerException);
                throw;
            }
        }

        public async Task<Guid> GetDeviceIdFromDataModel(Guid DataSourceID, IConfiguration config)
        {
            Debug.WriteLine("Datamodel Request: " + DataSourceID.ToString());
            UserToken token = new UserToken();
            dynamic savedToken = JsonConvert.DeserializeObject(Application.Current.Properties["Token"].ToString());
            token.Token = savedToken.Token;
            token.Expires = savedToken.Expires;
            token.TokenPrefix = savedToken.TokenPrefix;

            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization", token.TokenPrefix + token.Token);
            var url = config.BaseUrl + "api/imma/node/" + DataSourceID + "/data";
            try
            {
                var result =
                    await
                        client.PostAsync(url, null);
                var readableResponse = await ((StreamContent) result.Content).ReadAsStringAsync();
                JObject qwert = (JObject) JsonConvert.DeserializeObject(readableResponse);

                var data = qwert.First.First.First.First["Data"];

                var sensors = data["SensorData"];

                var x = sensors.First["DeviceID"];

                return Guid.Parse(x.ToString());


            }
            catch (System.Net.Http.HttpRequestException ex)
            {
                Debug.WriteLine("Exception Receiving DataModel:");
                Debug.WriteLine("- Message:" + ex.Message);
                Debug.WriteLine("- HasInner:" + ex.InnerException);
                throw;
            }
            catch (NullReferenceException ex)
            {
                Debug.WriteLine("EmptyDataModel");
            }

            return new Guid();
        }
    }
}
