using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "02/21/2017 13:42:59";
            var x = DateTime.Parse("2017-02-21");

            var date = s.Split(' ')[0];
            var year = date.Split('/')[2];
            var month = date.Split('/')[0];
            var day = date.Split('/')[1];

            var time = s.Split(' ')[1];
            var dt = $"{year}/{month}/{day} {time}";


            //var z = s.Split('-')[2].Split(' ')[0];
            var y = DateTime.Parse(dt);


            Console.WriteLine(y);

            Console.Read();
        }
    }
}
