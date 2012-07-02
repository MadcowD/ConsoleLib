using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    public interface IContainsComponents
    {
        void Add(string name, Component c);

        bool Remove(string name);

        void Get(string name, out Component tryComponent);

        void Set(string name, Component c);

    }
}
