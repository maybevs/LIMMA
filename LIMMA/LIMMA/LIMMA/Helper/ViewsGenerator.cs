using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Models;
using LIMMA.Views;
using Xamarin.Forms;

namespace LIMMA.Helper
{
    public static class ViewsGenerator
    {
        public static List<View> GenerateViewTest()
        {
            List<View> views = new List<View>();

            Label test = new Label();
            test.Text = "Label1";

            Label test2 = new Label();
            test2.Text = "Label2";


            ContentView cv = new ContentView()
            {
                Content = new Label() { Text = "Label3"}
            };

            SingleValue sv = new SingleValue("fu", new Settings());

            views.Add(test);
            views.Add(test2);
            views.Add(cv);
            views.Add(sv);
            return views;
        }

    }
}
