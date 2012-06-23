//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using ConsoleLib.UI.Modules;
//using ConsoleLib.UI;
//using System.Threading;
//using ConsoleLib.UI.Modules.Implementations;
//using ConsoleLib.UI.Components.Input;

//namespace ScreenTest
//{
//    public class TextBoxComponent : DrawableComponent, IRenderable, IHandlesInput
//    {
//        public TextBoxComponent(string Name, int p, int p_2, int p_3, int p_4, int p_5) :base(Name, p,p_2,p_3,p_4,p_5)
//        {
//            Delimiters = new List<Delimit>();
//        }

//        #region Functioning Loop
//        public override void Initialize()
//        {
//            Text = "";
//            WantsFocus = false;
//        }

//        public void Render()
//        {
//            ComponentEditor.Clear(this);
//            ComponentEditor.WriteString(this, Text);
//        }

//        #endregion

//        #region Variables
//        public string Text;
//        #endregion

//        #region Properties

     

//        #endregion

//        #region Helpers

//        #region IHandlesInput
//        public List<Delimit> Delimiters { get; set; }

//        public bool WantsFocus { get; set; }

//        public void HandleCurrentInput(string curr)
//        {
//            Text = curr;
//        }

//        #endregion

//        #region Delimiters

//        public bool Enter(ConsoleKeyInfo c, ref string current)
//        {
//            if (c.Key == ConsoleKey.Enter)
//                Console.Beep();

//            return true;
//        }

//        #endregion

//        #endregion


      
//    }
//}
