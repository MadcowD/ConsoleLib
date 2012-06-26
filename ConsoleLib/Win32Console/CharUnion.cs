using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;



namespace ConsoleLib.Win32Console
{
    /// <summary>
    /// A union between unicode characters and ascii characters imported from Win32
    /// for managed use.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharUnion
    {
        /// <summary>
        /// The specified unicode character within the union.
        /// </summary>
        [FieldOffset(0)]
        public char UnicodeChar;

        /// <summary>
        /// The specified ASCII character within the union.
        /// </summary>
        [FieldOffset(0)]
        public byte AsciiChar;
    }
}
