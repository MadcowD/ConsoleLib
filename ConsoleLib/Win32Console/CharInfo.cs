using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;


namespace ConsoleLib.Win32Console
{
    /// <summary>
    /// Character information (char + atttributes) imported from Win32
    /// for managed use.
    /// </summary>
    [StructLayout(LayoutKind.Explicit)]
    public struct CharInfo
    {
        /// <summary>
        /// The CharUnion containing the actual character.
        /// </summary>
        [FieldOffset(0)]
        public CharUnion Char;

        /// <summary>
        /// The attributes of the character.
        /// </summary>
        [FieldOffset(2)]
        public short Attributes;
    }
}
