using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    /// <summary>
    /// Specifies any class that is rederable in a loop
    /// </summary>
    public interface IRenderable
    {
        /// <summary>
        /// Renders the class accordingly with its own components
        /// </summary>
        /// <param name="TickTime">The current ticktime of the loop</param>
        void Render(int TickTime);
    }
}
