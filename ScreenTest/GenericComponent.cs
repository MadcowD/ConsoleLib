using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;
using ConsoleLib.UI;
using ConsoleLib.UI.Components;


namespace ScreenTest
{
    public class GenericComponent : DrawableComponent
    {

        public GenericComponent(string Name,int SizeX, int SizeY, int X, int Y, float Rotation) : base(Name, SizeX, SizeY, X, Y, Rotation)
        {
            ComponentEditor.ModifyAll(this,new Pixel(' ', ConsoleColor.Black, ConsoleColor.Red));
            ComponentEditor.ModifyBoarders(this);
        }

        public override void Initialize()
        {
            this.Foreground = ConsoleColor.Red;
        }
    }
}
