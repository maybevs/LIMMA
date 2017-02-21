using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Helper;
using Xamarin.Forms;

namespace LIMMA.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();

            //Todo: Read Configuration


            Title = "Window Title"; //Title
            BackgroundColor = Color.FromHex("#FFFFFF"); //Color
                                                        //BackgroundImage = 

            ContentView content = new ContentView();
            StackLayout layout = new StackLayout();

            var items = Helper.ViewsGenerator.GenerateViewTest();

            foreach (var item in items)
            {
                layout.Children.Add(item);
                
            }

            content.Content = layout;
            


            this.Content = content;
        }

        
    }
}
