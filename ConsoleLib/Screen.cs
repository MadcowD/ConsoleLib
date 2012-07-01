using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using System.Threading;
using System.IO;
using ConsoleLib.UI.Modules;
using ConsoleLib.UI;
using ConsoleLib.Win32Console;
using ConsoleLib.UI.Components;
using ConsoleLib.UI.Components.Input;


namespace ConsoleLib
{
    public class Screen
    {
        public Screen(string Title, IDrawableUnit Background)
        {
            //Set up component management
            Components = new Dictionary<string, Component>();
            InLoop = false;

            //Set up buffers
            PreBuffer = new IDrawableUnit[80, 25];
            Buffer = new CharInfo[80 * 25];

            //Set up screen properties
            this.Background = Background;
            this.ConsoleTitle = Title;

            //Set up input
            InputManager = new InputComponent(true, true);
            Add(InputManager);

            //Set up runtime
            Running = false;
        }

        #region Runtime

        private bool Running = false;

        /// <summary>
        /// Starts execution of the screen thread
        /// </summary>
        public void Start()
        {
            if (!h.IsInvalid)
            {
                Running = true;
                ScreenThread = new Thread(new ThreadStart(Run));
                ScreenThread.Start();
                InputManager.Asynchronous = true;
            }
        }

        /// <summary>
        /// Stops execution of the screen thread.
        /// </summary>
        public void Stop()
        {
            if (ScreenThread.IsAlive)
                Running = false;
            InputManager.Asynchronous = false;
        }




        /// <summary>
        /// Actual threading section
        /// </summary>
        public void Run()
        {
            while (Running)
            {
                InLoop = true;
                foreach (Component c in Components.Values)
                {
                    lock (c)
                    {
                        Update(c);
                        Render(c);
                    }
                }
                Draw();
                InLoop = false;
                Thread.Sleep(33);
            }

        }

        /// <summary>
        /// Update loop portion
        /// </summary>
        private void Update(Component c)
        {
            if (c is IHandlesInput)
                InputManager.AddFocus(c as IHandlesInput);

            if (c is IUpdatable)
                (c as IUpdatable).Update();
        }

        /// <summary>
        /// Render loop portion
        /// </summary>
        private void Render(Component c)
        {
            if (c is IRenderable)
                (c as IRenderable).Render();

            if (c is IDrawable)
                if ((c as IDrawable).DrawEnabled)
                    (c as IDrawable).Draw(PreBuffer, 25, 80);
        }

        private void Draw()
        {
            for (int y = 0; y < 25; y++)
                for (int x = 0; x < 80; x++)
                    if (PreBuffer[x, y] == null)
                        Buffer[y * 80 + x] = Background.Value;
                    else
                        Buffer[y * 80 + x] = PreBuffer[x, y].Value;

            SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = 80, Bottom = 25 }; //Create a rectangle for ConsoleWIndow

            bool b = WriteConsoleOutput(h, Buffer,
               new Coord() { X = 80, Y = 25 },
               new Coord() { X = 0, Y = 0 },
               ref rect);


            //Reset prebuffer
            PreBuffer = new IDrawableUnit[80, 25];
        }

        #endregion

        #region Variables

        //Thread
        private Thread ScreenThread;

        //Buffers
        private IDrawableUnit[,] PreBuffer;
        private CharInfo[] Buffer;

        //Input
        InputComponent InputManager;

        #endregion

        #region Properties
        public string ConsoleTitle
        {
            get
            {
                return Console.Title;
            }
            set
            {
                Console.Title = value;
            }
        }
        public IDrawableUnit Background { get; set; }
        #endregion

        #region Component Management

        /// <summary>
        /// The Component container
        /// </summary>
        private Dictionary<string, Component> Components;
        /// <summary>
        /// Specifies whether or not it is safe to add components
        /// </summary>
        bool InLoop;

        //All access functions
        #region Add

        /// <summary>
        /// Adds a component using its inherent name.
        /// </summary>
        /// <param name="c">The component to add.</param>
        public void Add(Component c)
        {
            while (InLoop) ;
            Components.Add(c.Name, c);
        }

        /// <summary>
        /// Adds a component using a specified name.
        /// </summary>
        /// <param name="componentName">The name under which the component will be.</param>
        /// <param name="c">The component to add.</param>
        public void Add(string componentName, Component c)
        {
            while (InLoop) ;
            Components.Add(componentName, c);
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes a component using a specified name.
        /// </summary>
        /// <param name="name">The name of the component to be removed.</param>
        /// <returns>Whether or not the component was removed (or even existed)</returns>
        bool Remove(string name)
        {
            while (InLoop) ;
            return Components.Remove(name);
        }

        /// <summary>
        /// Removes a specified component using its name.
        /// </summary>
        /// <param name="c">The component attempting to be removed.</param>
        /// <returns>If the component was removed (or even existed)</returns>
        bool Remove(Component c)
        {
            while (InLoop) ;
            return Components.Remove(c.Name);
        }

        #endregion

        #region Get

        /// <summary>
        /// Gets a component specified by its name.
        /// </summary>
        /// <param name="name">The name of the component to get.</param>
        /// <param name="tryComponent">The possible gotten component</param>
        public void Get(string name, out Component tryComponent)
        {
            Components.TryGetValue(name, out tryComponent);
        }

        #endregion

        #region Set
        /// <summary>
        /// Sets a component (or adds one if there is not one);
        /// </summary>
        /// <param name="name">The name of the component to set</param>
        /// <param name="c">The new component</param>
        public void Set(string name, Component c)
        {
            while (InLoop) ;

            Remove(name);
            Add(name, c);

        }

        #endregion

        #endregion

        #region Win32 Component

        private SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

        [DllImport("Kernel32.dll", SetLastError = true, CharSet = CharSet.Auto)]
        public static extern SafeFileHandle CreateFile(
            string fileName,
            [MarshalAs(UnmanagedType.U4)] uint fileAccess,
            [MarshalAs(UnmanagedType.U4)] uint fileShare,
            IntPtr securityAttributes,
            [MarshalAs(UnmanagedType.U4)] FileMode creationDisposition,
            [MarshalAs(UnmanagedType.U4)] int flags,
            IntPtr template);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool WriteConsoleOutput(
          SafeFileHandle hConsoleOutput,
          CharInfo[] lpBuffer,
          Coord dwBufferSize,
          Coord dwBufferCoord,
          ref SmallRect lpWriteRegion);

        #endregion
    }
}
