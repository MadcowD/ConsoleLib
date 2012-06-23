using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib;
using ConsoleLib.Win32Console;

namespace ConsoleLib.UI.Modules
{
    public interface IDrawableUnit
    {
        void ManagedDraw();

        CharInfo Value { get; set; }
        ConsoleColor BackColor { get; set; }
        ConsoleColor ForeColor { get; set; }

        void SetChar(char set);

    }
}
