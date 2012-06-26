using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI
{
    /// <summary>
    /// Specifies any class that is drawable
    /// </summary>
    public interface IDrawable
    {
        /// <summary>
        /// Draws the class to a buffer.
        /// </summary>
        /// <param name="buffer">The specific buffer to which the class will be drawn</param>
        /// <param name="Height">The height of the specified buffer.</param>
        /// <param name="Width">The width of the specified buffer.</param>
        void Draw(IDrawableUnit[,] buffer, int Height, int Width);

        /// <summary>
        /// If drawing is enabled.
        /// </summary>
        bool DrawEnabled { get; set; }

        /// <summary>
        /// If true, null characters shan't be drawn.
        /// </summary>
        bool Transparent { get; set; }

        /// <summary>
        /// The drawable contents of the class.
        /// </summary>
        IDrawableUnit[,] Contents { get; set; }
    }
}
