using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace ConsoleLib.Win32Console
{
    /// <summary>
    /// The win32 small rect class imported for managed use.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct SmallRect
    {
        /// <summary>
        /// The co-ordinate of the left point on the rectangle.
        /// </summary>
        public short Left;

        /// <summary>
        /// The co-ordinate of the top point on the rectangle.
        /// </summary>
        public short Top;

        /// <summary>
        /// The co-ordinate of the right point on the rectangle.
        /// </summary>
        public short Right;

        /// <summary>
        /// The co-ordinate of the bottom point on the rectangle.
        /// </summary>
        public short Bottom;
    }
}
