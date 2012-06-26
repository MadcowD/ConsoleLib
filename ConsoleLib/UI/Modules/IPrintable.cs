using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    /// <summary>
    /// Specifies any class that can be printed through managed methods.
    /// </summary>
    public interface IPrintable
    {
        /// <summary>
        /// Prints the class
        /// </summary>
        /// <param name="DrawThis"></param>
        void Print(IDrawableUnit[,] DrawThis);
    }
}
