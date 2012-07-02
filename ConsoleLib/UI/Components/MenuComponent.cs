using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Components.Input;
using ConsoleLib.UI.Modules;
using ConsoleLib.UI.Components.Group;

namespace ConsoleLib.UI.Components
{
    public class MenuComponent : DrawableGroupComponent, IHandlesInput, IUpdatable
    {
        public MenuComponent(string name)
            : base(name)
        {
            MenuPosition = 0;
            WantsFocus = true;
            Delimiters = new List<Delimit>();
            Delimiters.Add(Press);
            Delimiters.Add(Tab);
        }


        #region Functioning Loop

        public void Update()
        {
            //Add control of the menu back when the selected component isn't pressed.
            if (WantsFocus != true &&
                !SelectedComponent.IsPressed())
            {
                WantsFocus = true;
            }
        }

        #endregion

        #region Variables
        private int MenuPosition;
        public ISelectable SelectedComponent;

        #endregion

        #region Properties

        public List<Delimit> Delimiters { get; set; }
        public bool WantsFocus { get; set; }

        #endregion

        #region Helpers

        public void SelectNext()
        {
            lock (this)
            {
                SelectedComponent = Components.Values.First(a => a is ISelectable
                && a != SelectedComponent) as ISelectable;

                if (SelectedComponent == null) //If it can't find anything past that
                    SelectedComponent = Components.Values.First(a => a is ISelectable)
                        as ISelectable;
            }
        }

        #region IHandlesInput

        public void HandleCurrentInput(string current)
        {
            throw new NotImplementedException();
        }

        public bool Tab(ConsoleKeyInfo c, ref string current)
        {
            if (c.Key == ConsoleKey.Tab)
            {
                SelectedComponent.Unselect();
                SelectNext();
            }
            return false;

        }
        public bool Press(ConsoleKeyInfo c, ref string current)
        {
            if (c.Key == ConsoleKey.Spacebar ||
                c.Key == ConsoleKey.Enter)
                if (SelectedComponent != null)
                    SelectedComponent.Press();

            WantsFocus = false;
            return false;

        }

        #endregion

        #endregion

    }
}
