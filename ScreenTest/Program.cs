using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI;
using ConsoleLib;
using ConsoleLib.UI.Components;
using ConsoleLib.UI.Builders;
using System.Threading;
using System.Xml;
using ConsoleLib.UI.Components.Text;
using System.Windows.Forms;


namespace ScreenTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialize
            ConsoleMan.Initialize("ScreenTest", new Pixel(ConsoleColor.Black));
            KeyMan.Start();

            ConsoleComponent console = new ConsoleComponent("Text1", DrawableComponentBuilder.MakeConsole());
            TextComponent text = new TextComponent("CursorPosition", new
                DrawableComponent("CursorPosition", 20, 1, 20, 20, 0.0f));

            ConsoleMan.Add(console);
            ConsoleMan.Add(text);
            ConsoleMan.Start();
            #endregion

            Console.OutputEncoding = UTF32Encoding.UTF8;

            console.WriteLine("SHIT");
            string x = console.ReadLine();
            console.Write(x);
            while (true)
            {

                text.Text = System.Windows.Forms.Cursor.Position.ToString() + "\u0627";

            }

            ConsoleMan.Stop();
            KeyMan.Stop();

            return;

        }


    }
}
