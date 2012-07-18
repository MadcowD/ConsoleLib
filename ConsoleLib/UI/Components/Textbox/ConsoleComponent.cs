using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components.Textbox
{
    public class ConsoleComponent : TextBoxComponent, IHandlesKeyInput, IUpdatable
    {
        public ConsoleComponent(string Name, DrawableComponent drawInfo) :base(Name, drawInfo)
        {
            //Set up scrolling
            AutoScroll = true;

            //Set up input
            _CurrentInput = "";
            Reading = false;

            //Set up the contents of the console
            ConsoleText = new StringBuilder(Text);

            //Set up read/write streams
            Out = new StringWriter(ConsoleText);
            In = new StringReader(_CurrentInput);
        }

        #region Write 
        

        /// <summary>
        /// Write an object with arguments to the stream. {2}, etc.
        /// </summary>
        /// <param name="toWrite">The Object to be written to the stream</param>
        /// <param name="arg">The arguments for that object</param>
        public void WriteLine(object toWrite, params object[] arg)
        {
            Out.WriteLine(Convert.ToString(toWrite),arg);
        }

        /// <summary>
        /// Writes an object followed by a new line
        /// </summary>
        /// <param name="toWrite">The object to write</param>
        public void WriteLine(object toWrite)
        {
            Out.WriteLine(toWrite);
        }



        /// <summary>
        /// Write an object with arguments to the stream. {2}, etc.
        /// </summary>
        /// <param name="toWrite">The Object to be written to the stream</param>
        /// <param name="arg">The arguments for that object</param>
        public void Write(object toWrite, params object[] arg)
        {
            Out.Write(Convert.ToString(toWrite), arg);
        }

        /// <summary>
        /// Writes and object to the stream
        /// </summary>
        /// <param name="toWrite">The object to write</param>
        public void Write(object toWrite)
        {
            Out.Write(toWrite);
            
        }


        /// <summary>
        /// Clear the output stream.
        /// </summary>
        public void ClearOutput()
        {
            ConsoleText.Clear();
        }

        #endregion

        #region Read

        /// <summary>
        /// Reads a character from the console component
        /// </summary>
        /// <returns>The character read</returns>
        public char Read()
        {
            Reading = true;
            KeyMan.Focus(this);
            while (Reading) ; //Wait for user to enter input
            KeyMan.Unfocus();
            return (char)In.Read();
        }

        /// <summary>
        /// Reads a segment/string from input
        /// </summary>
        /// <returns>The read string</returns>
        public string ReadLine()
        {
            Reading = true;
            KeyMan.Focus(this);
            while (Reading) ; //Wait for user to enter input
            KeyMan.Unfocus();
            return In.ReadLine();
        }

        /// <summary>
        /// Attempts to read an Integer32
        /// </summary>
        /// <param name="tryRead">The integer that will be read (may fail)</param>
        /// <returns> Whether or not the parse was successful</returns>
        public bool ReadInt(out int tryRead)
        {
            return int.TryParse(ReadLine(), out tryRead);
        }

        /// <summary>
        /// Attempts to read an doubleeger32
        /// </summary>
        /// <param name="tryRead">The doubleeger that will be read (may fail)</param>
        /// <returns> Whether or not the parse was successful</returns>
        public bool ReadDouble(out double tryRead)
        {
            return double.TryParse(ReadLine(), out tryRead);
        }

        #endregion


        #region Functioning Loop

        /// <summary>
        /// Updates the console
        /// </summary>
        public void Update()
        {

            lock(Text)
                Text = ConsoleText.ToString() + _CurrentInput;
        }

        #endregion

        #region Variables

        private string _CurrentInput;
        private bool Reading;
        protected StringBuilder ConsoleText;

        #endregion

        #region Properties

        public StringWriter Out {set;get;}
        public StringReader In { set; get; }




        #endregion

        #region Helpers

        #region IHandlesInput

        public void HandleCurrentInput(ConsoleKeyInfo Key)
        {
            switch (Key.Key)
            {
                case ConsoleKey.Backspace:
                    _CurrentInput = 
                        _CurrentInput.Remove(_CurrentInput.Length - 1);
                    break;
                case ConsoleKey.Enter:
                    Reading = false;
                    break;
                default:
                    _CurrentInput += Key.KeyChar;
                    break;
            }
        }

        #endregion

        #endregion

    }
}
