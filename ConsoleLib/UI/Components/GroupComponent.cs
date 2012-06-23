﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components
{
    public class GroupComponent : Component, ITransformable
    {
        public GroupComponent(string Name)
            : base(Name)
        {

        }



        #region Functioning Loop

        public override void Initialize()
        {
            Contains = new List<ITransformable>();
            X = 0;
            Y = 0;
        }

        #endregion

        #region Variables

        public List<ITransformable> Contains;

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
                foreach (ITransformable val in Contains)
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

                foreach (ITransformable val in Contains)
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