using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace ConsoleLib.UI.Components
{
    /// <summary>
    /// Base texture component class
    /// </summary>
    public class TextureComponent : DrawableComponent
    {

        #region Functioning Loop
        #endregion

        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Helpers

        /// <summary>
        /// Loads a texture from a file
        /// </summary>
        /// <param name="filename">The file from which to get the texture</param>
        /// <returns>If it loaded the texture correctly</returns>
        public bool Load(string filename){

            FileStream fs = new FileStream(filename, FileMode.Open);
            BinaryReader br = new BinaryReader(fs);

            //Read the size
            int sizex = br.ReadInt32();
            int sizey = br.ReadInt32();

            this.Scale(sizex, sizey); //Scale to new size
            ComponentEditor.ModifyAll(this,null); //And clear

            //Read textures
            int totalPixels = br.ReadInt32();

            for (int i = 0; i < totalPixels; i++ )
            {
                int x = br.ReadInt32();
                int y = br.ReadInt32();
                char c = (char)br.ReadInt32();
                ConsoleColor f = (ConsoleColor)br.ReadInt32();
                ConsoleColor b = (ConsoleColor)br.ReadInt32();
                Contents[x, y] = new Pixel(c, f, b);

            }
            return true;

        }

        /// <summary>
        /// Saves the texture to a file
        /// </summary>
        /// <param name="filename">The specified file to which the texture will be saved</param>
        /// <returns></returns>
        public bool Save(string filename)
        {
            FileStream fs = new FileStream(filename, FileMode.Create);
            BinaryWriter bw = new BinaryWriter(fs);

            //First write the size of the texture
            bw.Write(SizeX);
            bw.Write(SizeY);

            //Determine amount of pixels for reader
            int totalPixels = 0;
            //Write each texture
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    if(Contents[x,y]!= null) //if the pixel isn't null, write the pixel
                        totalPixels++;

            //Write pixels lol
            bw.Write(totalPixels);
            for (int x = 0; x < SizeX; x++)
                for (int y = 0; y < SizeY; y++)
                    if (Contents[x, y] != null)
                    { //if the pixel isn't null, write the pixel
                        bw.Write(x);
                        bw.Write(y);
                        bw.Write((int)Contents[x, y].Value.Char.UnicodeChar);
                        bw.Write((int)Contents[x, y].ForeColor);
                        bw.Write((int)Contents[x, y].BackColor);
                    }


            return true;
        }

        #endregion

    }
}
