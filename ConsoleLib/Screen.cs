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
        public Screen(int TickWait, IDrawableUnit Background, string Title)
        {
            this.Width = 80;
            this.Height = 25;
            Components = new Dictionary<string, Component>();
            _PreBuffer = new IDrawableUnit[Width, Height];
            _Buffer = new CharInfo[Width * Height];


            this.Background = Background;
            this.ConsoleTitle = Title;

            InputManager = new InputComponent(true, true);
            AddComponent(InputManager);
            Stop = false;
        }

        #region Runtime
        public void Start()
        {
            if (!h.IsInvalid)
            {
                _RenderThread = new Thread(new ThreadStart(RenderLoop));
                _RenderThread.Start();
            }

            _UpdateThread = new Thread(new ThreadStart(UpdateLoop));
            _UpdateThread.Start();
        }

        private void RenderLoop(){
            while (!Stop){
                //Draw

                _PreBuffer = new IDrawableUnit[Width, Height];
                foreach (Component c in Components.Values)
                {
                    lock (c)
                    {
                        if(c is IRenderable)
                            (c as IRenderable).Render();
                        if(c is IDrawable)
                            (c as IDrawable).Draw(_PreBuffer, Height, Width);
                    }
                }
                //Convert
                    for (int y = 0; y < Height; y++)
                        for (int x = 0; x < Width; x++)
                            if (_PreBuffer[x, y] == null)
                                _Buffer[y * Width + x] = Background.Value;
                            else
                                _Buffer[y * Width + x] = _PreBuffer[x, y].Value;

                        SmallRect rect = new SmallRect() { Left = 0, Top = 0, Right = 80, Bottom = 25 }; //Create a rectangle for ConsoleWIndow

                        bool b = WriteConsoleOutput(h, _Buffer,
                           new Coord() { X = 80, Y = 25 },
                           new Coord() { X = 0, Y = 0 },
                           ref rect);


               Thread.Sleep(TickWait);
            }
        }

        private void UpdateLoop(){
            while (!Stop)
            {
                foreach (Component c in Components.Values)
                {
                    lock (c)
                    {
                        if (c is IHandlesInput)
                            InputManager.AddFocus(c as IHandlesInput);

                        if (c is IUpdatable)
                            (c as IUpdatable).Update();


                    }
                }

                Thread.Sleep(TickWait);
            }

            InputManager.Asynchronous = false;
        }
        #endregion

        #region Variables

        private CharInfo[] _Buffer;
        IDrawableUnit[,] _PreBuffer;
        private Thread _RenderThread;
        private Thread _UpdateThread;
        private SafeFileHandle h = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, FileMode.Open, 0, IntPtr.Zero);



        InputComponent InputManager;
        private Dictionary<string, Component> Components;

        #endregion

        #region Properties

        public int Width { get; set; }
        public int Height { get; set; }

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

        public bool Stop { get; set; }
        public int TickWait { get; set; }



        #endregion

        #region Helpers

        public void AddComponent(Component c){

            Components.Add(c.Name, c);
        }

        public Component GetComponent(string name)
        {
            Component ret;
            Components.TryGetValue(name, out ret);
            return ret;
        }



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
