using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    public interface IScalable
    {
        int SizeX { get; set; }
        int SizeY { get; set; }

        void Scale(int newX, int newY);
    }
}
