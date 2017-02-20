using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace LIMMA.Views
{
    public class SingleValueView : ContentPage
    {

        public SingleValueView()
        {
            this.Content = new ContentView();
            {
                Content = new Label { Text = "Hello World" };
            };
        }
    }
}
