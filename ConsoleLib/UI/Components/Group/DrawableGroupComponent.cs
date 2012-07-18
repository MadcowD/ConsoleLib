using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Components.Group
{
    public class DrawableGroupComponent : GroupComponent
    {
        public DrawableGroupComponent(string name)
            : base(name)
        {
        }

        #region ComponentManagement
        public virtual void Hide()
        {
            foreach (IDrawable currentComponent in Components.Values)
            {
                currentComponent.DrawEnabled = false;
            }
        }

        public virtual void Show()
        {
            foreach (IDrawable currentComponent in Components.Values)
            {
                currentComponent.DrawEnabled = true;
            }
        }
        #endregion
    }
}
