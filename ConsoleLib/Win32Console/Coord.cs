using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ConsoleLib.Win32Console
{
    /// <summary>
    /// Win32 Coord struct imported for managed use.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Coord
    {
        /// <summary>
        /// The X Coordinate
        /// </summary>
        public short X;
        /// <summary>
        /// The Y Coordinate
        /// </summary>
        public short Y;

        /// <summary>
        /// Creates a new Coord under the specified parameters.
        /// </summary>
        /// <param name="X">The new X</param>
        /// <param name="Y">The new Y</param>
        public Coord(short X, short Y)
        {
            this.X = X;
            this.Y = Y;
        }
    };
}
