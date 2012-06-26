using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib;
using ConsoleLib.Win32Console;

namespace ConsoleLib.UI.Modules
{
    /// <summary>
    /// Specifies any class that can be drawn
    /// </summary>
    public interface IDrawableUnit
    {
        /// <summary>
        /// Draws the class using managed methods
        /// </summary>
        void ManagedDraw();


        /// <summary>
        /// The actual character value of the class.
        /// </summary>
        CharInfo Value { get; set; }

        /// <summary>
        /// The background color attribute of the class.
        /// </summary>
        ConsoleColor BackColor { get; set; }

        /// <summary>
        /// The foregroung color attribute of the class.
        /// </summary>
        ConsoleColor ForeColor { get; set; }


        /// <summary>
        /// Sets the value in terms of a character.
        /// </summary>
        /// <param name="set">Sets the character value of the class.</param>
        void SetChar(char set);

    }
}
