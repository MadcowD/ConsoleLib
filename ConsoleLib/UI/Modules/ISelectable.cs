using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    public interface ISelectable
    {
        void Select();
        void Unselect();
        void Press();

        bool IsPressed();
    }
}
