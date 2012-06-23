using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using ConsoleLib.UI.Components.Input;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components.Textboxes
{
    public class ConsoleComponent : TextBoxComponent, IHandlesInput, IUpdatable
    {
        public ConsoleComponent(string Name, DrawableComponent drawInfo) :base(Name, drawInfo)
        {
            _CurrentInput = "";
            Reading = false;
            WantsFocus = true;

            consoleText = new StringBuilder(Text);

            Out = new StringWriter(consoleText);
            In = new StringReader(_CurrentInput);
            Delimiters = new List<Delimit>();

            Delimiters.Add(this.Enter);
            Delimiters.Add(InputComponent.BackspaceAndDelete);
        }

        #region Read/Write 
        public void WriteLine(object toWrite, params object[] arg)
        {
            Out.WriteLine(toWrite as string,arg);
        }

        public void WriteLine(object toWrite)
        {
            Out.WriteLine(toWrite);
        }


        public void Write(object toWrite, params object[] arg)
        {
            Out.Write(toWrite as string, arg);
        }

        public void Write(object toWrite)
        {
            Out.Write(toWrite);
            
        }



        public char Read()
        {
            Reading = true;
            while (Reading) ;
            return (char)In.Read();
        }

        public string ReadLine()
        {
            Reading = true;
            while (Reading) ;
            return In.ReadLine();
        }

        public void Clear()
        {
            consoleText.Clear();
        }
        #endregion

        #region Functioning Loop

        public override void Render()
        {
            ComponentEditor.Clear(this);
            ComponentEditor.WriteString(this, _VisibleText + _CurrentInput, DrawStartX, DrawStartY, DrawEndX, DrawEndY);
        }

        public void Update()
        {
            lock(Text)
                Text = consoleText.ToString();
        }

        #endregion

        #region Variables

        private string _CurrentInput;
        private bool Reading;
        protected StringBuilder consoleText;

        #endregion

        #region Properties

        public StringWriter Out {set;get;}
        public StringReader In { set; get; }


        #endregion

        #region Helpers

        #region IHandlesInput

        public void HandleCurrentInput(string current)
        {
            _CurrentInput = current;
        }

        #region Delimeters

        public bool Enter(ConsoleKeyInfo c, ref string current)
        {
            if (c.Key == ConsoleKey.Enter)
            {
                In = new StringReader(current);
                WriteLine(_CurrentInput);
                current = "";
                Reading = false;
            }


            if (Reading)
                return true;
            else
                return false;
        }

        #endregion

        #endregion

        #endregion


        public List<Delimit> Delimiters { get; set; }
        public bool WantsFocus { get; set; }



    }
}
