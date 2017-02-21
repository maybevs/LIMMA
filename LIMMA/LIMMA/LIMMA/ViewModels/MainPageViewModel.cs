using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Helper;
using LIMMA.Interfaces;
using Prism.Mvvm;

namespace LIMMA.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        private IConnectionServices connection;
        public string TestString => connection.UserToken.Token;

        public MainPageViewModel(IConnectionServices connection, IConfiguration config)
        {
            this.connection = connection;
            //var task = connection.GetCurrentToken(config);
            //var awaiter = task.GetAwaiter();
            //var token = awaiter.GetResult();
            

            
        }

    }
}
