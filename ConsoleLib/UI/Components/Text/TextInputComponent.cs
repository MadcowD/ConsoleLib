using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components.Text
{
    class TextInputComponent : TextComponent, IHandlesKeyInput
    {
        /// <summary>
        /// The TextInput constructor
        /// </summary>
        /// <param name="name">name of the component</param>
        /// <param name="drawInfo">Drawing info about the text component</param>
        /// <param name="inputActionCallBack">The function to be called whence enter is pressed.</param>
        public TextInputComponent(string name, DrawableComponent
            drawInfo, stringCallBack inputActionCallBack)
            : base(name, drawInfo)
        {
            _sCurrentInput = "";
            _inputActionCallBack = inputActionCallBack;
        }


        #region Functioning Loop
        #endregion

        #region Variables

        delegate void stringCallBack(string CurrentInput);

        private string _sCurrentInput;
        private stringCallBack _inputActionCallBack;


        #endregion

        #region Properties

        public string CurrentInput
        {
            get
            {
                return _sCurrentInput;
            }
        }

        #endregion

        #region Helpers

        public virtual void HandleCurrentInput(ConsoleKeyInfo Key)
        {
            if (Key.Key == ConsoleKey.Enter)
            {
                _inputActionCallBack(_sCurrentInput);
                _sCurrentInput = "";
            }
            else if (Key.Key == ConsoleKey.Backspace)
            {
                _sCurrentInput =
                     _sCurrentInput.Remove(_sCurrentInput.Length - 1);
            }
            else
                _sCurrentInput += Key.KeyChar;
        }

        #endregion


    }
}
