using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI
{
    public interface IDrawable
    {
        void Draw(IDrawableUnit[,] buffer, int Height, int Width);

        bool DrawEnabled { get; set; }
        bool Transparent { get; set; }
        IDrawableUnit[,] Contents { get; set; }
    }
}
