using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Components.Input
{
    public delegate bool Delimit(ConsoleKeyInfo c, ref string CurrentInput);
}
