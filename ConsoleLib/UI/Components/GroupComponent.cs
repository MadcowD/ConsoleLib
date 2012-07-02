using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components
{
    public class GroupComponent : Component, ITransformable, IContainsComponents
    {
        public GroupComponent(string Name)
            : base(Name)
        {
            Components = new Dictionary<string, Component>();
            X = 0;
            Y = 0;
        }

        #region Functioning Loop
        public override void Initialize()
        {

        }
        #endregion

        #region Component Management

        /// <summary>
        /// The Component container
        /// </summary>
        protected Dictionary<string, Component> Components;

        //All access functions
        #region Add

        /// <summary>
        /// Adds a component using its inherent name.
        /// </summary>
        /// <param name="c">The component to add.</param>
        public void Add(Component c)
        {
            Components.Add(c.Name, c);
        }

        /// <summary>
        /// Adds a component using a specified name.
        /// </summary>
        /// <param name="componentName">The name under which the component will be.</param>
        /// <param name="c">The component to add.</param>
        public void Add(string componentName, Component c)
        {
            Components.Add(componentName, c);
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes a component using a specified name.
        /// </summary>
        /// <param name="name">The name of the component to be removed.</param>
        /// <returns>Whether or not the component was removed (or even existed)</returns>
        public bool Remove(string name)
        {
            return Components.Remove(name);
        }

        /// <summary>
        /// Removes a specified component using its name.
        /// </summary>
        /// <param name="c">The component attempting to be removed.</param>
        /// <returns>If the component was removed (or even existed)</returns>
        public bool Remove(Component c)
        {
            return Components.Remove(c.Name);
        }

        #endregion

        #region Get

        /// <summary>
        /// Gets a component specified by its name.
        /// </summary>
        /// <param name="name">The name of the component to get.</param>
        /// <param name="tryComponent">The possible gotten component</param>
        public void Get(string name, out Component tryComponent)
        {
            Components.TryGetValue(name, out tryComponent);
        }

        #endregion

        #region Set
        /// <summary>
        /// Sets a component (or adds one if there is not one);
        /// </summary>
        /// <param name="name">The name of the component to set</param>
        /// <param name="c">The new component</param>
        public void Set(string name, Component c)
        {
            Remove(name);
            Add(name, c);

        }

        #endregion

        #endregion

        #region Variables

        private int _X;
        private int _Y;

        #endregion

        #region Properties

        public int X
        {
            get
            {
                return _X;
            }
            set
            {

                int calcVal = Math.Abs(value);
                foreach (ITransformable val in Components.Values)
                    lock(val)
                    val.X = Math.Abs(val.X - _X + calcVal);

                _X = calcVal;
            }
        }

        public int Y
        {
            get
            {
                return _Y;
            }
            set
            {
                int calcVal = Math.Abs(value);

                foreach (ITransformable val in Components.Values)
                    lock(val)
                    val.Y = Math.Abs(val.Y - _Y + calcVal);

                _Y = calcVal ; 
            }
        }

        #endregion

        #region Helpers


        #endregion
    }
}
