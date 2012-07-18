using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;
using ConsoleLib.UI.Components.Group;

namespace ConsoleLib.UI.Components
{
    //public class MenuComponent : DrawableGroupComponent, IUpdatable
    //{
    //    public MenuComponent(string name)
    //        : base(name)
    //    {
    //        MenuPosition = 0;
    //        WantsFocus = true;
    //    }


    //    #region Functioning Loop

    //    public void Update()
    //    {
    //        //Add control of the menu back when the selected component isn't pressed.
    //        if (WantsFocus != true &&
    //            !SelectedComponent.IsPressed())
    //        {
    //            WantsFocus = true;
    //        }
    //    }

    //    #endregion

    //    #region Variables
    //    private int MenuPosition;
    //    public ISelectable SelectedComponent;

    //    #endregion

    //    #region Properties

    //    #endregion

    //    #region Helpers

    //    public void SelectNext()
    //    {
    //        lock (this)
    //        {
    //            SelectedComponent = Components.Values.FirstOrDefault(a => a is ISelectable
    //                && a != SelectedComponent) as ISelectable;
    //            SelectedComponent.Select();

    //            if (SelectedComponent == null) //If it can't find anything past that
    //                SelectedComponent = Components.Values.First(a => a is ISelectable)
    //                    as ISelectable;
    //        }
    //    }

    //    #region IHandlesInput

    //    public void HandleCurrentInput(string current)
    //    {
    //    }

    //    public bool Tab(ConsoleKeyInfo c, ref string current)
    //    {
    //        if (c.Key == ConsoleKey.Tab)
    //        {
    //            if(SelectedComponent != null)
    //                SelectedComponent.Unselect();
    //            SelectNext();
    //        }
    //        return false;

    //    }
    //    public bool Press(ConsoleKeyInfo c, ref string current)
    //    {
    //        if (c.Key == ConsoleKey.Spacebar ||
    //            c.Key == ConsoleKey.Enter)
    //            if (SelectedComponent != null)
    //            {
    //                SelectedComponent.Press();
    //                WantsFocus = false;
    //            }
    //        return false;

    //    }

    //    #endregion

    //    public override void Hide()
    //    {
    //        base.Hide();
    //        WantsFocus = false;
    //    }

    //    public override void Show()
    //    {
    //        base.Show();
    //        WantsFocus = true;
    //    }
        
    //    #endregion

    //}
}
