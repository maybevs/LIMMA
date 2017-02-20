using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Interfaces;

namespace LIMMA.Services
{
    public class ConnectionService : IConnectionServices
    {
        public async Task<string> GetConnection(IConfiguration config)
        {
            var response = await Helper.UserToken.UpdateToken(config);


            return response;
        }

    }
}
