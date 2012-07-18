using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ConsoleLib.UI.Modules
{
    public interface IHandlesMouseInput
    {
        void onMouseEvent(object sender, MouseEventArgs e,
            int consolex, int consoley);
    }
}
