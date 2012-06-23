using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    public interface IPrintable
    {
        void Print(IDrawableUnit[,] DrawThis);
    }
}
