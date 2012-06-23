using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;
using ConsoleLib.UI.Components;

namespace ConsoleLib.UI
{
    public static class ComponentEditor
    {
        public static void ModifyAll(DrawableComponent Component, IDrawableUnit Modification)
        {
            for (int x = 0; x < Component.SizeX; x++)
                for (int y = 0; y < Component.SizeY; y++)
                    Component.Contents[x, y] = Modification;
        }



        public static void Clear(DrawableComponent Component)
        {
            for (int x = 0; x < Component.SizeX; x++)
                for (int y = 0; y < Component.SizeY; y++)
                    Component.Contents[x, y] = new Pixel(Component.Background);
        }//Do not use every tick

        public static void ModifyBoarders(DrawableComponent Component)
        {
            for (int x = 0; x < Component.SizeX; x++)
                for (int y = 0; y < Component.SizeY; y++)
                {
                    if (x == 0 || x == Component.SizeX - 1 ||
                        y == 0 || y == Component.SizeY - 1) //If it's on the boarders
                        Component.Contents[x, y] = new Pixel(Component.Foreground);

                }
        }
   

        public static void WriteString(DrawableComponent Component, string Str, int X, int Y, int SizeX, int SizeY)
        {
            if (Str != null)
            {

                int i = 0;
                for (int y = Y; y < SizeY  && i < Str.Length; y++)
                    for (int x = X; x < SizeX  && i < Str.Length; x++)
                    {
                        if (Str[i] == '\n')
                        {
                            y++;
                            x = X-1; //Because x is incremented next time, set it to 1 below the original X position
                        }
                        else if(Str[i] != '\r' && i < Str.Length && y < Component.SizeY && x < Component.SizeX) //Fucking '\r'
                            Component.Contents[x, y] = new Pixel(Str[i], Component.Foreground, Component.Background);
                        i++;
                    }
            }
        }

        public static void WriteString(DrawableComponent Component, string Str)
        {
            WriteString(Component, Str, 0, 0, Component.SizeX, Component.SizeY);
        }

        public static void WriteStringInBoarder(DrawableComponent Component, string Str)
        {
            WriteString(Component, Str, 1, 1, Component.SizeX-1, Component.SizeY-1);
        }



    }
}
