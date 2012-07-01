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
using System.Xml;

namespace ScreenTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Initialize
            Screen sc = new Screen("ScreenTest", new Pixel(ConsoleColor.Black));
            ConsoleComponent console = new ConsoleComponent("Text1", DrawableComponentBuilder.MakeConsole());
            sc.Add(console);
            sc.Start();
            #endregion

            console.WriteLine("SHIT");
            XmlTextReader reader = new XmlTextReader("../../xmlfile.xml");
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element: // The node is an element.
                        console.Write("<" + reader.Name);
                        console.Write(">");
                        break;
                    case XmlNodeType.Text: //Display the text in each element.
                        console.Write(reader.Value);
                        break;
                    case XmlNodeType.EndElement: //Display the end of the element.
                        console.Write("</" + reader.Name);
                        console.Write(">");
                        break;
                }
            }

            Thread.Sleep(1000);
            sc.Stop();
            return;

        }


    }
}
