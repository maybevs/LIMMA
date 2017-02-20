using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Helper;
using LIMMA.Interfaces;

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

        public ConnectionService(IConfiguration config)
        {

        }

        public async Task<UserToken> GetToken(IConfiguration config)
        {
            //Todo: Check if token is in Storage, else get a new one.
            //Todo: If approach exp. Date renew.


            var response = await Helper.UserToken.GetToken(config);

            return response;
            
        }

    }
}
