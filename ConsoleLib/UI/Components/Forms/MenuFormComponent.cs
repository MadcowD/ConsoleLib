using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components.Forms
{
    public abstract class MenuFormComponent : FormComponent, IUpdatable
    {
        #region Functioning Loop
        #endregion

        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Helpers

        /// <summary>
        /// Registers the components herein contained to the menu.
        /// </summary>
        /// <param name="component">The component to be registered.</param>
        protected virtual override void RegisterComponent(Component component)
        {

            base.RegisterComponent(component);
        }

        /// <summary>
        /// Unregisters a component from the menu.
        /// </summary>
        /// <param name="component"></param>
        protected virtual override void UnregisterComponent(Component component)
        {

            base.RegisterComponent(component);
        }

        #endregion
    }
}
