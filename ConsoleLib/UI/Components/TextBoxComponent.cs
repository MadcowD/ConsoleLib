using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;
using ConsoleLib.UI.Components.Input;

namespace ConsoleLib.UI.Components
{
    public class TextBoxComponent : DrawableComponent, IRenderable
    {

        public TextBoxComponent(String Name, DrawableComponent drawInfo)
            : base(Name, drawInfo)
        {
                        _VisibleText = "";
            _Text = "";
            VisibleLines = SizeX;
            CurrentLine = 0;
            DrawStartX = 0;
            DrawStartY = 0;
            DrawEndX = SizeX;
            DrawEndY = SizeY;
        }

        #region Functioning Loop


        public virtual void Render()
        {
            ComponentEditor.Clear(this);
            ComponentEditor.WriteString(this, _VisibleText,DrawStartX,DrawStartY, DrawEndX, DrawEndY);
        }


        #endregion

        #region Variables
        private string _Text;
        protected string _VisibleText;
        #endregion

        #region Properties

        public string Text
        {
            get { return _Text; }
            set
            {
                _Text = value;
                CalculateVisibleText();
            }
        }

        public int VisibleLines { get; set; }
        public int CurrentLine { get; set; }
        public int DrawStartX { get; set; }
        public int DrawStartY { get; set; }
        public int DrawEndX { get; set; }
        public int DrawEndY { get; set; }

        #endregion

        #region Helpers

        private void CalculateVisibleText() //TODO: REWRITE
        {
            lock (Text)
            {
                //int visibleLines = -1;
                //int linesInText = 0;
                //string outputString = "";


                ////Variables for ease of code visibility
                //int height = DrawEndY - DrawStartY;
                //int width = DrawEndX - DrawStartX;
                //int length = height * width;

                //// Loop through all text to calculate text
                //for (int i = 0; //Iteration is equal to the current line times the width
                //    i < _Text.Length && //Whilst iteration is less than the length
                //    i < length && //Whilst iteration is less than the size of the text box
                //    visibleLines < VisibleLines &&
                //    linesInText < height+1; i++)
                //{
                //    if (linesInText < CurrentLine)
                //        outputString = "";

                //    if (_Text[i] == '\n')//If there is going to be a new line, increase the visible lines count
                //    {
                //        linesInText++;
                //        if (!(linesInText < CurrentLine))
                //        visibleLines++;
                //        outputString += '\n';
                //    }
                //    else //If it's just a normal character
                //    {
                //        if (i % width == 0){ ///If it's time for a new line
                //            linesInText++;
                //            if (!(linesInText < CurrentLine))
                //                visibleLines++;
                //        }
                            
                //        outputString += _Text[i];
                //    }
                //}




                //_VisibleText = outputString;


            }
        }

        

        #region IScalable

        public void Scale(int X, int Y){


            DrawEndX += X-SizeX;
            DrawEndY += Y-SizeY;

            base.Scale(X, Y);
        }

        #endregion

        #endregion


    }
}
