using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;
using ConsoleLib.UI.Components.Input;
using System.Threading;

namespace ConsoleLib.UI.Components
{
    public class InputComponent : Component, IUpdatable
    {

        public InputComponent(bool _Async, bool _AcceptInput) : base("INPUT")
        {
            Asynchronous = _Async;
            AcceptingInput = _AcceptInput;

            focusQueue = new Queue<IHandlesInput>();
        }

        public void GetInput()
        {
            if (AcceptingInput && focus != null)
            {
                bool addToBuffer = true;
                ConsoleKeyInfo c = Console.ReadKey();
                foreach (Delimit delFunc in focus.Delimiters)
                    if (delFunc(c, ref CurrentInput) == false)
                        addToBuffer = false;

                if (addToBuffer)
                    CurrentInput += c.KeyChar;

                focus.HandleCurrentInput(CurrentInput);
            }
        }

        public void AddFocus(IHandlesInput item)
        {
            if(item.WantsFocus && !focusQueue.Contains(item))
                focusQueue.Enqueue(item);
        }


        #region Functioning Loop

        public override void Initialize()
        {
            Name = "INPUT";
            CurrentInput = "";

            //Set up thread
            asyncThread = new Thread(new ThreadStart(delegate()
            {
                while (Asynchronous)
                {
                    GetInput();
                }
            }));
        }

        public void Update()
        {
            //Focus Queue stuff
            if ((focus == null || !focus.WantsFocus) && focusQueue.Count > 0)
            {
                focus = focusQueue.Dequeue();
                CurrentInput = "";
            }



            //Threading stuff
            if (!Asynchronous)
            {
                GetInput();
            }
            else
                if (asyncThread.ThreadState != ThreadState.Running)
                    asyncThread.Start();

                  
        }

        #endregion 

        #region Variables

        private Thread asyncThread;
        private IHandlesInput focus;
        private Queue<IHandlesInput> focusQueue;

        public string CurrentInput;

        #endregion

        #region Properties

        public bool AcceptingInput { get; set; }
        public bool Asynchronous { get; set; }


        #endregion

        #region Helpers

        #region Premade Delimiters

        public static bool BackspaceAndDelete(ConsoleKeyInfo c, ref string current)
        {
            if (c.Key == ConsoleKey.Backspace && current.Length > 0)
                current = current.Remove(current.Length - 1);
            else if (c.Key == ConsoleKey.Delete)
                current = "";
            else
                return true;

            return false;
        }

        #endregion

        #endregion
    }
}
