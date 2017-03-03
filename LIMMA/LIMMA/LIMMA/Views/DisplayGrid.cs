using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LIMMA.Models;
using Xamarin.Forms;

namespace LIMMA.Views
{
    public class DisplayGrid : ContentView
    {
        public string Name { get; set; }

        public Grid Grid { get; set; }

        public DisplayGrid(string id, Settings Settings)
        {
            Name = id;

            Grid = new Grid();

            for (int i = 0; i < 12; i++)
            {
                ColumnDefinition cd = new ColumnDefinition();
                Grid.ColumnDefinitions.Add(cd);
            }


            Content = Grid;
        }
    }
}
