using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/*WHEN IMPLEMENTING SUBCLASSES*/
/* Use the following code to define the internals of the class:

        #region Functioning Loop
        #endregion 

        #region Variables
        #endregion

        #region Properties  
        #endregion

        #region Helpers
        #endregion

 * * * * * * * * * * * * * * * *
 * Functioning Loop:
 *  If implements IUpdatable, IRenderable, IHandlesInput,
 *   then place implementation here.
 *  
 * Helpers:
 *  If implements any other interfaces,
 *   then place implementations here within their
 *   own regions (#region IExample #endregion)
*/


namespace ConsoleLib.UI
{
    public abstract class Component
    {
        public Component(string Name)
        {
            this.Name = Name;
            Initialize();
        }

        public abstract void Initialize();

        #region Functioning Loop
        #endregion 

        #region Variables
        #endregion

        #region Properties

        public string Name { get; set; }
        
        #endregion

        #region Helpers
        #endregion

    }
}

