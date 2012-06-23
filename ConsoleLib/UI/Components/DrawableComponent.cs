using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components
{
    public class DrawableComponent : Component, IDrawable, ITransformable, IRotatable, IScalable
    {

        
        public DrawableComponent(string Name, int SizeX, int SizeY, int X, int Y, float Rotation) : base(Name)
        {
            this.Contents = new IDrawableUnit[SizeX, SizeY];
            this.SizeX = SizeX;
            this.SizeY = SizeY;
            this.X = X;
            this.Y = Y;
            this.Rotation = Rotation;
            this.DrawEnabled = true;
        }

        public DrawableComponent(string Name, DrawableComponent instance)
            : base(Name)
        {
            this.Contents = new IDrawableUnit[instance.SizeX, instance.SizeY];
            this.SizeX = instance.SizeX;
            this.SizeY = instance.SizeY;
            this.X = instance.X;
            this.Y = instance.Y;
            this.Rotation = instance.Rotation;
            this.DrawEnabled = instance.DrawEnabled;
            this.Background = instance.Background;
            this.Foreground = instance.Foreground;
            this.Transparent = instance.Transparent;
        }

        public DrawableComponent() : base("DEFAULT_DRAWABLECOMPONENT")
        {
            this.SizeX = 1;
            this.SizeY = 1;
            this.Contents = new IDrawableUnit[SizeX, SizeY];
            this.X = 0;
            this.Y = 0;
            this.Rotation = 0.0f;
            this.DrawEnabled = true;
            this.Background = ConsoleColor.White;
            this.Foreground = ConsoleColor.Black;
            this.Transparent = false;
        }

        public override void Initialize()
        {

        }


        #region Properties
        /// <summary>
        /// Whether or not drawing is enabled.
        /// </summary>
        public bool DrawEnabled { get; set; }

        /// <summary>
        /// The transparent property of a 
        /// drawable component determines whether
        /// or not null IDrawableUnits should be
        /// drawn.
        /// </summary>
        public bool Transparent { get; set; }

        /// <summary>
        /// The default background pixel to draw
        /// if transparency is off
        /// and a null pixel is being drawn.
        /// </summary>
        public ConsoleColor Background { get; set; }

        /// <summary>
        /// The default foregound pixel to draw.
        /// </summary>
        public ConsoleColor Foreground { get; set; }


        /// <summary>
        /// The contents of the DrawableComponent.
        /// </summary>
        public IDrawableUnit[,] Contents { get; set; }

        /// <summary>
        /// The X coordinate of the component.
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// The Y coordinate of the component.
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// The width of the component.
        /// </summary>
        public int SizeX { get; set; }

        /// <summary>
        /// The height of the component.
        /// </summary>
        public int SizeY { get; set; }

        /// <summary>
        /// The rotation in float degrees of the component.
        /// </summary>
        public float Rotation { get; set; }

        #endregion

        #region Helpers

        #region IRotatable

        /// <summary>
        /// Calculates an output buffer of IDrawableUnit
        /// based on the DrawableComponent's current rotation.
        /// 
        /// This function uses the formula:
        ///  * x' = xcos(degrees) - ysin(degrees)
        ///  * y' = xsin(degrees) + ycos(degrees)
        /// to find specific output points on a rotated buffer.
        /// 
        ///  1. Rotation occurs in the center,
        ///  2. Rotated points which are outside of the original
        ///     buffer's size are clipped.
        ///  3. The buffer does not scale.
        /// </summary>
        /// <returns>The function returns a rotated version of
        /// DrawableComponent.Contents.</returns>
        public IDrawableUnit[,] CalculateRotation()
        {
            double cosd = Math.Round(Math.Cos(this.Rotation)); //Cosine of the degrees to rotate
            double sind = Math.Round(Math.Sin(this.Rotation)); //Sine of the degrees to rotate

            int centerx = SizeX / 2;
            int centery = SizeY / 2;

            IDrawableUnit[,] rotatedContent = new IDrawableUnit[SizeX, SizeY];

            //Rotate
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                {
                    /*Math for rotation
                     * x' = xcos(degrees) - ysin(degrees)
                     * y' = xsin(degrees) + ycos(degrees)
                     */
                    int xprime = centerx + (int)((x - centerx) * cosd - (y - centery) * sind);
                    int yprime = centery + (int)((x - centerx) * sind + (y - centery) * cosd);

                    //Determine if is within boundries
                    if ((xprime >= 0 && xprime < SizeX && yprime >= 0 && yprime < SizeY))
                    {

                        rotatedContent[xprime, yprime] = this.Contents[x, y];
                    }
                }


            return rotatedContent;

        }

        #endregion

        #region IScalable

        /// <summary>
        /// Scales the current DrawableComponent to a new size
        /// whilst maintaing content which should not be clipped.
        /// </summary>
        /// <param name="newX">The new unsigned width.</param>
        /// <param name="newY">The new unsigned height.</param>
        public void Scale(int newX, int newY)
        {
            IDrawableUnit[,] temporaryContent = new IDrawableUnit[newX, newY];

            for (int x = 0; x < newX; x++)
            {
                for (int y = 0; y < newY; y++)
                {
                    //Check to see if still in bounds of old size
                    if (y < SizeY && x < SizeX)
                        temporaryContent[x, y] = this.Contents[x, y];
                    else
                        temporaryContent[x, y] = null; //If it is out of bounds of original content place a null pixel here
                }
            }

            //Set the sizes and content
            SizeX = newX;
            SizeY = newY;
            this.Contents = temporaryContent;
        }
            
        #endregion

        #region IDrawable

        /// <summary>
        /// Draws the specified component to a IDrawableUnit[,] buffer.
        /// </summary>
        /// <param name="Buffer">The buffer to which the method will draw.</param>
        /// <param name="Height">The height of the buffer to be drawn to.</param>
        /// <param name="Width">The width of the buffer to be drawn to.</param>
        public void Draw(IDrawableUnit[,] Buffer, int Height, int Width)
        {
            lock(this)
            if (X < Width && Y < Height) //If the Component is within the bounds of the Buffer
            {

                for (int y = Y; y < Height && y - Y < SizeY; y++)
                {
                    for (int x = X; x < Width && x - X < SizeX; x++)
                    {   //If the content is not null (only if transparent)
                        if (Contents[x - X, y - Y] != null)
                            Buffer[x, y] = Contents[x - X, y - Y]; //set the Buffer
                        else if (!Transparent)
                            Buffer[x, y] = new Pixel(Background);
                        else if (Transparent)
                            Buffer[x, y] = null;
                    }
                }
            }
        }

        #endregion

        #endregion

    }
}
