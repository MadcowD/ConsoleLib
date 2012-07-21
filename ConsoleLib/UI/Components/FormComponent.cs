using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components
{
    public abstract class FormComponent : Component
    {
        #region Functioning Loop
        #endregion

        #region Variables
        #endregion

        #region Properties
        #endregion

        #region Helpers

        /// <summary>
        /// Registers the components herein contained.
        /// </summary>
        /// <param name="component">The component to be registered.</param>
        protected virtual void RegisterComponent(Component component)
        {
            ConsoleMan.Add(component);
        }

        /// <summary>
        /// Unregisters a component.
        /// </summary>
        /// <param name="component"></param>
        protected virtual void UnregisterComponent(Component component)
        {
            ConsoleMan.Remove(component);
        }

        #endregion
    }
}
