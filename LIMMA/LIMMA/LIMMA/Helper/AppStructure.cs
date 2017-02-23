using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Interfaces;
using LIMMA.Models;
using Newtonsoft.Json;
using Xamarin.Forms;


namespace LIMMA.Helper
{
    public class AppStructure
    {
        //Todo: Appstruktur mit Nodes

        public Tenant Tenant { get; set; }

        public static async Task<AppStructure> GetAppStructure(IConfiguration config)
        {
            UserToken token = new UserToken();

            dynamic savedToken = JsonConvert.DeserializeObject(Application.Current.Properties["Token"].ToString());
            token.Token = savedToken.Token;
            token.Expires = savedToken.Expires;
            token.TokenPrefix = savedToken.TokenPrefix;


            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Add("Authorization",token.TokenPrefix + token.Token);

            var result =
                await
                    client.GetStringAsync(config.BaseUrl + "api/imma/application/AD23BF06-7EB3-4669-9157-74A9B4EF2611");

            dynamic asdf = JsonConvert.DeserializeObject(result);

            AppStructure app = new AppStructure();


            return app;
        }
    }
}
