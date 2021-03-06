﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.Win32Console;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI
{
    /// <summary>
    /// Pixel class, the basic of all componets
    /// </summary>
    public class Pixel : IDrawableUnit
    {
        /// <summary>
        /// Set the initial contents of the Pixel
        /// </summary>
        /// <param name="Value">The character that this Pixel represents</param>
        /// <param name="ForeColor">The forecolor that the pixel will take on.</param>
        /// <param name="BackColor">The backcolor that the pixel will take on.</param>
        public Pixel(char Value, ConsoleColor ForeColor, ConsoleColor BackColor)
        {
            this.Value = new Win32Console.CharInfo() { Char = { UnicodeChar = Value } };
            this.ForeColor = ForeColor;
            this.BackColor = BackColor;
        }



        /// <summary>
        /// A single color pixel with a ' ' char value
        /// </summary>
        /// <param name="SingleColor">The color</param>
        public Pixel(ConsoleColor SingleColor)
        {
            this.Value = new Win32Console.CharInfo() { Char = {UnicodeChar = ' ' } };
            this.ForeColor = SingleColor;
            this.BackColor = SingleColor;
        }

        /// <summary>
        /// Specifies the default Pixel.
        /// </summary>
        public Pixel()
        {
            this.Value = new Win32Console.CharInfo() { Char = { UnicodeChar = ' ' } };
            this.ForeColor = ConsoleColor.Gray;
            this.BackColor = ConsoleColor.Black;
        }

        #region Properties

        /// <summary>
        /// The actual character value of the pixel.
        /// </summary>
        public CharInfo Value { get; set; }


        /// <summary>
        /// Gets or sets the background Value.Attribute of the pixel through a ConsoleColor struct.
        /// ConsoleColor is converted into the proper byte form.
        /// </summary>
        /// <seealso cref="ConsoleLib.Win32Console.CharInfo"/>
        public ConsoleColor BackColor
        {
            get
            {
                //Converts the ConsoleColor struct from a formatted short
                /*   Example conversion:
                 *  0xF344 (Attribute value)
                 *    0x0F44 >> 4   = 0x00F4 (Shifts)
                 *    0x00F4 &  0xF = 0x0004 (Seperates)
                 *    return 15;
                */
                return (ConsoleColor)((Value.Attributes >> 4)& 0xF);
            }
            set
            {
                //Converts a ConsoleColor to a formatted short
                /*   Example conversion:
                 *  ConsolColor.Red            = 4;
                 *  0x0004           << 4      = 0x0040;
                 *  Value.Attributes &  0x0040 = 0x814F;
                 *  Value.Attributes           = 0x814F;
                */
                Value = new CharInfo() { Attributes = (short)(Value.Attributes | ((short)value) << 4), Char = { UnicodeChar = this.Value.Char.UnicodeChar } };
            }
        }

        /// <summary>
        /// Gets or sets the foreground Value.Attribute of the pixel through a ConsoleColor struct.
        /// ConsoleColor is converted into the proper byte form.
        /// </summary>
        /// <seealso cref="ConsoleLib.Win32Console.CharInfo"/>
        public ConsoleColor ForeColor
        {
            get
            {
                //Returns only the last hexadecimal digit of Value.Attributes
                return (ConsoleColor)((Value.Attributes) & 0xF);
            }
            set
            {
                //Sets only the last hexadecimal digit of Value.Attributes
                Value = new CharInfo() { Attributes = (short)(Value.Attributes | (short)value), Char = {UnicodeChar = this.Value.Char.UnicodeChar } };

            }
        }
        
        #endregion

        #region Helpers
        /// <summary>
        /// Prints the pixel to the console (NOT EFFICIENT)
        /// </summary>
        public void ManagedDraw()
        {
            Console.BackgroundColor = BackColor;
            Console.ForegroundColor = ForeColor;
            Console.Write(Value.Char.UnicodeChar);
            Console.ResetColor();
        }
        /// <summary>
        /// Returns the literal char value of Pixel
        /// </summary>
        /// <returns>Writes the pixel to the screen</returns>
        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// Sets the value of the character.
        /// </summary>
        /// <param name="set">The new unicode character to be set.</param>
        public void SetChar(char set)
        {
            this.Value = new Win32Console.CharInfo() { Char = {  UnicodeChar = set }, Attributes = this.Value.Attributes};
        }

        #endregion

    }
}
