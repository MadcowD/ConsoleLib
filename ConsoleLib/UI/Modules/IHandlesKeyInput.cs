﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    public interface IHandlesKeyInput
    {
        void HandleCurrentInput(ConsoleKeyInfo Key);
    }
}
