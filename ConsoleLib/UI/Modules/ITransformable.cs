using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    /// <summary>
    /// Specifies any class that is transformable on a XY axis.
    /// </summary>
    public interface ITransformable
    {
        /// <summary>
        /// The X coordinate
        /// </summary>
        int X { get; set; }

        /// <summary>
        /// The Y coordinate
        /// </summary>
        int Y { get; set; }
    }
}
