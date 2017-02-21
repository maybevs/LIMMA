using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;

using Xamarin.Forms;

namespace LIMMA.Views
{
    public class SingleValue : ContentView
    {
        public SingleValue()
        {
            Content = new Label { Text = "Hello ContentView" };
            
        }
    }
}
