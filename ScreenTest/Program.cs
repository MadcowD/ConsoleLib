using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI;
using ConsoleLib;
using ConsoleLib.UI.Components;
using ConsoleLib.UI.Builders;
using System.Threading;
using ConsoleLib.UI.Components.Textboxes;

namespace ScreenTest
{
    class Program
    {
        static void Main(string[] args)
        {

            Screen sc = new Screen(33, new Pixel(ConsoleColor.Black), "ScreenTest");
            
            //Make the builder
            DrawableComponentBuilder builder = new DrawableComponentBuilder();

            //Build Components
            //Text1
            builder.setPosition(0, 20);
            builder.setSize(80, 5);
            builder.setBackground(ConsoleColor.Black);
            builder.setForeground(ConsoleColor.Red);
            builder.setTransparent(true);
            ConsoleComponent console = new ConsoleComponent("Text1", builder.Make() as DrawableComponent);
            builder.Reset();


            //Add components
            sc.AddComponent(console);


            sc.Start();


            //CONSOLE TEST :)
            console.WriteLine("Please enter your age:");

            string line = console.ReadLine();

            int x;
            while (!int.TryParse(line,out x))

            {

                console.WriteLine("This is not a valid number. Please enter again:");

                line = console.ReadLine();

            }



            console.WriteLine("you just wrote {0}", x);

            console.Read();
            console.Clear();

            console.Write("Tits");


            Thread.Sleep(200);
            sc.Stop = true;

        }


    }
}
