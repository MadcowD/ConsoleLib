using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleLib.UI.Builders
{
    public interface IBuilder
    {
		object Make();
		void Reset();


        #region Setters

        #endregion

    }
}
