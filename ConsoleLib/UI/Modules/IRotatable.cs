using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    /// <summary>
    /// Specifies any class that can rotate.
    /// </summary>
    public interface IRotatable
    {
        /// <summary>
        /// The rotation of that class.
        /// </summary>
        float Rotation { get; set; }

        /// <summary>
        /// The result of the rotation calculated
        /// </summary>
        /// <returns>The calculated rotation</returns>
        IDrawableUnit[,] CalculateRotation();
    }
}
