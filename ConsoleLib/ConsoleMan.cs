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
using System.Windows.Forms;


namespace ConsoleLib
{
    /// <summary>
    /// The console manager.
    /// </summary>
    public static class ConsoleMan
    {
        public static void Initialize(string Titlez, IDrawableUnit Backgroundz)
        {
            //Set up component management
            Components = new Dictionary<string, Component>();
            InLoop = false;

            //Set up buffers
            PreBuffer = new IDrawableUnit[80, 25];
            Buffer = new CharInfo[80 * 25];

            //Set up screen properties
            Background = Backgroundz;
            ConsoleTitle = Titlez;


            //Set up runtime
            Running = false;
        }

        #region Runtime

        private static bool Running = false;

        /// <summary>
        /// Starts execution of the screen thread
        /// </summary>
        public static void Start()
        {
            if (!h.IsInvalid)
            {
                Running = true;
                ScreenThread = new Thread(new ThreadStart(Run));
                ScreenThread.Start();
            }
        }

        /// <summary>
        /// Actual threading section
        /// </summary>
        public static void Run()
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
        /// Stops execution of the screen thread.
        /// </summary>
        public static void Stop()
        {
            if (ScreenThread.IsAlive)
                Running = false;
        }






        /// <summary>
        /// Update loop portion
        /// </summary>
        private static void Update(Component c)
        {
            if (c is IUpdatable)
                (c as IUpdatable).Update();
        }
        /// <summary>
        /// Render loop portion
        /// </summary>
        private static void Render(Component c)
        {
            if (c is IRenderable)
                (c as IRenderable).Render();

            if (c is IDrawable)
                if ((c as IDrawable).DrawEnabled)
                    (c as IDrawable).Draw(PreBuffer, 25, 80);
        }

        private static void Draw()
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

        #region Events

        public static void HandleMouseEvents(object sender, MouseEventArgs e)
        {
            //Convert mouse location into pixel locations in consoleman


            //Grab all components that can handle mouse input,
            //can have scale an position, and are containing the location
            //of the mouse event.
            foreach (KeyValuePair<string,Component> kvpc in Components.Where(kvp =>
                kvp.Value is IHandlesMouseInput &&
                kvp.Value is ITransformable &&
                kvp.Value is IScalable &&
                (kvp.Value as ITransformable).X <= e.X &&
                (kvp.Value as ITransformable).Y <= e.Y &&
                (kvp.Value as ITransformable).X +
                    (kvp.Value as IScalable).SizeX >= e.X &&
                (kvp.Value as ITransformable).Y +
                    (kvp.Value as IScalable).SizeY >= e.Y))
            {
                (kvpc.Value as IHandlesMouseInput).onMouseEvent(sender, e, 0, 0);
            }

            ConsoleMan.ConsoleTitle = e.Y.ToString();
        }

        #endregion

        #region Variables

        //Thread
        private static Thread ScreenThread;

        //Buffers
        private static IDrawableUnit[,] PreBuffer;
        private static CharInfo[] Buffer;

        //Runtime ticks
        public static  int TickTime = 0;

        #endregion

        #region Properties
        public static string ConsoleTitle
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
        public static IDrawableUnit Background { get; set; }
        #endregion

       #region Component Management

        /// <summary>
        /// The Component container
        /// </summary>
        private static Dictionary<string, Component> Components;
        /// <summary>
        /// Specifies whether or not it is safe to add components
        /// </summary>
        static bool InLoop;

        //All access functions
        #region Add

        /// <summary>
        /// Adds a component using its inherent name.
        /// </summary>
        /// <param name="c">The component to add.</param>
        public static void Add(Component c)
        {
            while (InLoop) ;
            Components.Add(c.Name, c);
        }

        /// <summary>
        /// Adds a component using a specified name.
        /// </summary>
        /// <param name="componentName">The name under which the component will be.</param>
        /// <param name="c">The component to add.</param>
        public static void Add(string componentName, Component c)
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
        public static bool Remove(string name)
        {
            while (InLoop) ;
            return Components.Remove(name);
        }

        /// <summary>
        /// Removes a specified component using its name.
        /// </summary>
        /// <param name="c">The component attempting to be removed.</param>
        /// <returns>If the component was removed (or even existed)</returns>
        public static bool Remove(Component c)
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
        public static void Get(string name, out Component tryComponent)
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
        public static void Set(string name, Component c)
        {
            while (InLoop) ;

            Remove(name);
            Add(name, c);

        }

        #endregion

        #endregion 

        #region Win32 Component

        #region interface

        /// <summary>
        /// Sets the position of the console window
        /// </summary>
        /// <param name="x">The x position of the console.</param>
        /// <param name="y">The y position of the console.</param>
        public static void SetWindowPos(int x, int y)
        {
            SetWindowPos(Handle, 0, x, y, 0, 0, SWP_NOSIZE);
        }

        /// <summary>
        /// Gets the current top and left position of the console.
        /// </summary>
        /// <param name="x">The left position of the console.</param>
        /// <param name="y">The top position of the console.</param>
        public static void GetWindowPos(out int x, out int y)
        {
            SmallRect tempRec;
            GetWindowRect(Handle, out tempRec);

            x = tempRec.Left;
            y = tempRec.Right;
        }

        #endregion

        #region Handle


        [DllImport("kernel32.dll", ExactSpelling = true)]
        private static  extern IntPtr GetConsoleWindow();

        public static IntPtr Handle = GetConsoleWindow();

        #endregion

        #region Buffer
        public static SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);

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

        #region GetWindowRect
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static  extern bool GetWindowRect(IntPtr hWnd, out SmallRect lpRect);
        #endregion

        #region SetWindowPos
        const int SWP_NOSIZE = 0x0001;

        [DllImport("user32.dll", EntryPoint = "SetWindowPos")]
        public static extern IntPtr SetWindowPos(IntPtr hWnd, int hWndInsertAfter, int x, int Y, int cx, int cy, int wFlags);
        #endregion

        #endregion
    }
}
