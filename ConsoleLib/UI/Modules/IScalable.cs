using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    /// <summary>
    /// Specifies any class that is scalable or has a size.
    /// </summary>
    public interface IScalable
    {
        /// <summary>
        /// The width of the scalable class
        /// </summary>
        int SizeX { get; set; }

        /// <summary>
        /// The height of the scalable class
        /// </summary>
        int SizeY { get; set; }


        /// <summary>
        /// Scale the object according to all of its other components
        /// </summary>
        /// <param name="newX">The new height</param>
        /// <param name="newY">The new width</param>
        void Scale(int newX, int newY);
    }
}
