﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Modules
{
    public interface IRotatable
    {
        float Rotation { get; set; }

        IDrawableUnit[,] CalculateRotation();
    }
}
