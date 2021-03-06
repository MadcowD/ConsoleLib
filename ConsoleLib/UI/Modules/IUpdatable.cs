﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    /// <summary>
    /// The IUpdatable interfaces specifies any class which can be updated in a loop.
    /// </summary>
    public interface IUpdatable 
    {

        /// <summary>
        /// Updates the inherenting class.
        /// </summary>
        /// <param name="TickTime">The current ticktime of the loop</param>
        void Update(int TickTime);
    }
}
