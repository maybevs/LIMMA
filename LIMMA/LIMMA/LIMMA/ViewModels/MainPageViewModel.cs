using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Interfaces;
using Prism.Mvvm;

namespace LIMMA.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public string TestString { get; set; }
        
        public MainPageViewModel(IConnectionServices connection, IConfiguration config)
        {
            TestString = connection.GetConnection(config).Result;
        }

    }
}
