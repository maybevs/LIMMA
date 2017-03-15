using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using LIMMA.Helper;
using LIMMA.Models;
using Xamarin.Forms;

namespace LIMMA.Views
{
    public class SingleValue : ContentView
    {
        public string Name { get; set; }
        public Label TextDisplay { get; set; }
        public SingleValue(string id, Settings Settings)
        {
            Name = id;


            TextDisplay = new Label();
            TextDisplay.BackgroundColor = AppStructureHelpers.GetColor(Settings.BackgroundColor);
            TextDisplay.Text = "1234567890ßßßßßßßßßßßß";
            TextDisplay.HorizontalTextAlignment = TextAlignment.End;


            Content = TextDisplay;






        }
    }
}
