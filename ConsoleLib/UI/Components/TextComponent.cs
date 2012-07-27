using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components
{
    /// <summary>
    /// A TextComponent with which text can be displayed to the Screen.
    /// </summary>
    public class TextComponent : DrawableComponent, IRenderable
    {

        public TextComponent(String Name, DrawableComponent drawInfo)
            : base(Name, drawInfo)
        {
            _VisibleText = "";
            _Text = "";
            AutoScroll = false;
        }

        #region Functioning Loop


        public virtual void Render(int TickTime)
        {
            ComponentEditor.Clear(this);
            ComponentEditor.WriteString(this, _VisibleText, 0, 0, SizeX, SizeY);
        }


        #endregion

        #region Variables
        /// <summary>
        /// The instantiated variable containing the raw text.
        /// </summary>
        private string _Text;

        /// <summary>
        /// The calculated visual text with which will be displayed in the textbox. SEE SCROLL.
        /// </summary>
        protected string _VisibleText;
        #endregion

        #region Properties

        /// <summary>
        /// The text containted within the textbox.
        /// </summary>
        public string Text //TODO: Possibly add colored text within the textbox.
        {
            get { return _Text; }
            set
            {
                _Text = value;
                Scroll();
            }
        }

        /// <summary>
        /// The position with which the visible area is determined
        /// </summary>
        public int ScrollPosition { get; set; }

        /// <summary>
        /// If the Scroll Position should be automatically determined at the end of the Text.
        /// </summary>
        public bool AutoScroll { get; set; }

        #endregion

        #region Helpers

        /// <summary>
        /// Calculates the visual range that is going to be displayed if the text is larger than the visible area.
        /// </summary>
        public void Scroll()
        {
            //Convert text into a list
            List<string> textLines = new List<string>();

            int lineCharCount = 0;
            string line = "";
            //Loop through it
            foreach (char currentCharacter in Text)
            {
                line += currentCharacter; //Add the character to the line
                lineCharCount++; //Increase the count of characters this line

                if (lineCharCount >= SizeX || currentCharacter == '\n') //If the character marks the start of a new line
                {
                    textLines.Add(line); //Add the line to the list of lines
                    
                    //Reset the variables
                    line = "";
                    lineCharCount = 0;
                }

            }
            //Add the last line
            textLines.Add(line);

            //If text is actually larger than the visual area then calculate a scroll
            if (textLines.Count > SizeY)
            {
                //Set the _VisibleText
                if (AutoScroll) //If AutoScroll
                    ScrollPosition = textLines.Count - SizeY; // Set it to the end

                //Normalize the ScrollPosition
                if (ScrollPosition < 0)
                    ScrollPosition = 0;

                _VisibleText = ""; //Reset the VisibleText

                //Grab the lines withing the ScrollPosition + the visual range
                for (int i = ScrollPosition; i < textLines.Count && i < ScrollPosition + SizeY; i++)
                {
                    _VisibleText += textLines.ElementAt(i);
                }
            }
            else
                _VisibleText = Text;

        }




        #endregion


    }
}
