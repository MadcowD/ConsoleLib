using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Components;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Builders
{
    public class DrawableComponentBuilder : IBuilder
    {
        public DrawableComponentBuilder()
        {
            build = new DrawableComponent();
        }

        public object Make()
        {
            return new DrawableComponent(build.Name, build);
        }

        public static DrawableComponent MakeConsole()
        {
            return new DrawableComponent("Console", 80, 25, 0, 0, 0.0f);
        }
        public void Reset()
        {

        }

        #region Setters

        public void setSize(int Width, int Height)
        {
            build.SizeX = Width;
            build.SizeY = Height;
        }

        public void setPosition(int X, int Y)
        {
            build.X = X;
            build.Y = Y;
        }

        public void setBackground(ConsoleColor Background)
        {
            build.Background = Background;
        }

        public void setForeground(ConsoleColor Foreground)
        {
            build.Foreground = Foreground;
        }

        public void setTransparent(bool Transparent)
        {
            build.Transparent = Transparent;
        }

        #endregion



        private DrawableComponent build;
    }
}
