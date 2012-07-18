using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using ConsoleLib.UI.Modules;

namespace ConsoleLib
{
    /// <summary>
    /// Keyboard input manager for ConsoleLib.
    /// </summary>
    public static class KeyMan
    {
        #region Runtime

        #region Variables
        /// <summary>
        /// An internal boolean that specifies whether or not input
        /// has been checked with TryGetKey previously.
        /// </summary>
        private static bool FreshInput = false;

        /// <summary>
        /// Keeps the thread running
        /// </summary>
        private static bool Running = false;

        /// <summary>
        /// The CurrentKey recieved from the run time loop.
        /// </summary>
        private static ConsoleKeyInfo CurrentKey;

        private static Thread KeyManThread = new Thread(
            new ThreadStart(Run));
        
        #endregion

        /// <summary>
        /// Starts the KeyManager thread.
        /// </summary>
        public static void Start()
        {
            KeyManThread.Start();
            Running = true;
        }

        /// <summary>
        /// The main thread loop for input.
        /// </summary>
        private static void Run()
        {
            while (Running)
            {
                CurrentKey = Console.ReadKey();
                FreshInput = true;
                if (Focused)
                    CallFoci();
            }
        }

        /// <summary>
        /// Stops the main KeyManger thread.
        /// </summary>
        public static void Stop()
        {
            Running = false;
            KeyManThread.Abort();
        }

        #endregion

        #region Focused Input

        #region Variables
        /// <summary>
        /// An internal boolean that specifies whether or not input is being focused.
        /// </summary>
        private static bool Focused = false;

        /// <summary>
        /// The list of foci to which the KeyManager will call when input is recieved.
        /// </summary>
        private static List<IHandlesKeyInput> Foci =
            new List<IHandlesKeyInput>();

        #endregion

        /// <summary>
        /// Updates all foci on new input recieved.
        /// </summary>
        private static void CallFoci()
        {
            for(int i = 0; i < Foci.Count; i++)
                Foci[i].HandleCurrentInput(CurrentKey);
        }

        /// <summary>
        /// Adds a focus to the list of foci.
        /// Focused objects are constantly notified when new input is added to
        /// the stream.
        /// </summary>
        /// <param name="focus">The object to focus.</param>
        public static void Focus(IHandlesKeyInput focus)
        {
            Foci.Add(focus);
            Focused = true;
        }

        /// <summary>
        /// Unfocuses all foci in the manager, reverts input back to normal.
        /// </summary>
        public static void Unfocus()
        {
            Foci = new List<IHandlesKeyInput>();
            Focused = false;
        }


        #endregion

        #region GetKey


        /// <summary>
        /// Attempts to get the current key.
        /// </summary>
        /// <param name="Key">The current key if exists.</param>
        /// <returns>Whether or not the key was recieved.</returns>
        public static bool TryGetKey(out ConsoleKeyInfo Key)
        {
            if (!Focused && FreshInput)
            {
                FreshInput = false;
                Key = CurrentKey;
                return true;
            }
            else
            {
                Key = new ConsoleKeyInfo();
                return false;
            }
        }

        /// <summary>
        /// Gets the last key pressed regardless of focus.
        /// </summary>
        /// <returns>Returns the last key pressed.</returns>
        public static ConsoleKeyInfo GetLastKey()
        {
            return CurrentKey;
        }

        #endregion
    }
}
