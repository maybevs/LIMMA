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
        public string GetConnection()
        {
            return "Hallo Welt";
        }
    }
}
