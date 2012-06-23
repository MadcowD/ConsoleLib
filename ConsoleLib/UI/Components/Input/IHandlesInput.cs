using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Components.Input
{

    public interface IHandlesInput
    {
        List<Delimit> Delimiters { get; set; }

        bool WantsFocus { get; set; }

        void HandleCurrentInput(string current);

    }
}
