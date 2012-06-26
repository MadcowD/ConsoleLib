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
            builder.setPosition(0, 0);
            builder.setSize(30, 3);
            builder.setBackground(ConsoleColor.Green);
            builder.setForeground(ConsoleColor.Red);
            builder.setTransparent(true);
            ConsoleComponent console = new ConsoleComponent("Text1", builder.Make() as DrawableComponent);
            builder.Reset();


            //Add components
            sc.AddComponent(console);
            sc.Start();


            //CONSOLE TEST :)
            console.Write("Please enter your age:");

            

            int x;
            while (!console.ReadInt(out x))
                console.Write("This is not a valid number. Please enter again:");




            console.WriteLine("you just wrote {0}", x);

            console.Read();
            console.ClearOutput();

            console.Write("Tits");


            Thread.Sleep(200);
            sc.Stop = true;

        }


    }
}
