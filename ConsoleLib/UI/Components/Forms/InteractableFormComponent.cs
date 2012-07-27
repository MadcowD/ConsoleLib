using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components.Forms
{
    public abstract class InteractableFormComponent : FormComponent
    {
        public InteractableFormComponent(string name)
            : base(name)
        {
        }



        #region Functioning Loop
        #endregion

        #region Variables

        /// <summary>
        /// The list of selectable components
        /// </summary>
        private List<ISelectable> _registeredSelectableComponents = new List<ISelectable>();
        /// <summary>
        /// Represents the current selected component
        /// </summary>
        private ISelectable _selectedComponent = null;

        /// <summary>
        /// The current index that the _selected component is located at within the list;
        /// </summary>
        private int _iIndex = 0;


        #endregion

        #region Properties
        #endregion

        #region Helpers

        /// <summary>
        /// Registers the components herein contained.
        /// </summary>
        /// <param name="component">The component to be registered.</param>
        protected override void RegisterComponent(Component component)
        {
            if (component is ISelectable && component != null &&
                !_registeredSelectableComponents.Contains(component as ISelectable))
                _registeredSelectableComponents.Add(component as ISelectable);
            if (_selectedComponent != null)
                _iIndex = _registeredSelectableComponents.IndexOf(_selectedComponent);
            base.RegisterComponent(component);
        }

        /// <summary>
        /// Unregisters a component.
        /// </summary>
        /// <param name="component">The component to attempt to unregister</param>
        protected override void UnregisterComponent(Component component)
        {
            if (component is ISelectable && component != null)
                _registeredSelectableComponents.Remove(component as ISelectable);
            if (_selectedComponent != null)
                _iIndex = _registeredSelectableComponents.IndexOf(_selectedComponent);
            base.UnregisterComponent(component);
        }

        /// <summary>
        /// Selects the next selectable component in the form.
        /// </summary>
        protected virtual void SelectNextComponent()
        {
            if (_registeredSelectableComponents.Count > 0) //If there are components to select
            {
                if (_selectedComponent != null) //Deselect the last component
                    _selectedComponent.Deselect();

                //Find the first component that isn't the current component or default
                _iIndex++;
                if(_iIndex < _registeredSelectableComponents.Count)
                _selectedComponent = _registeredSelectableComponents[_iIndex]; //Grab the next component
                else if (_iIndex >= _registeredSelectableComponents.Count()) //If the for loop reached the end of the list
                {
                    _iIndex = 0; //Set the index to the beginning
                    _selectedComponent = _registeredSelectableComponents[0]; //Grab the first component
                }


                if (_selectedComponent != null) //Select the new selected component
                    _selectedComponent.Select();
            }
        }

        /// <summary>
        /// Presses the current selected component
        /// </summary>
        protected virtual void PressSelectedComponent()
        {
            if (_selectedComponent != null)
            {
                _selectedComponent.Press();
                _selectedComponent.Select();
            }
        }


        #endregion

    }
}
