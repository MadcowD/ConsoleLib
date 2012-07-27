using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components.Text
{
    public class ButtonComponent : TextComponent, IRenderable, ISelectable
    {
        public ButtonComponent(string Name, DrawableComponent drawInfo,
            IDrawableUnit SelectedColors, IDrawableUnit PressedColors,
            IDrawableUnit UnselectedColors,
            PressAction calledWhenPressed)
            : base(Name,drawInfo)
        {
            this.SelectedColors = SelectedColors;
            this.UnselectedColors = UnselectedColors;
            this.PressedColors = PressedColors;

            Pressed = false;
            Selected = false;
            SetColors(UnselectedColors);
            this.calledWhenPressed = calledWhenPressed;
        }

        #region Functioning Loop
        public void Render(int TickTime)
        {
            base.Render(TickTime);
            ComponentEditor.ModifyBoarders(this);
        }
        #endregion

        #region Variables

        bool Selected;
        IDrawableUnit SelectedColors;
        IDrawableUnit PressedColors;
        IDrawableUnit UnselectedColors;

        #endregion

        #region Properties

        public bool Pressed { get; set; }

        #endregion

        #region Helpers

        private void SetColors(IDrawableUnit colors)
        {
            this.Foreground = colors.ForeColor;
            this.Background = colors.BackColor;
        }

        #region ISelectable
        public delegate void PressAction();
        public PressAction calledWhenPressed;

        public bool IsPressed()
        {
            return Pressed;
        }

        public void Select()
        {
            Selected = true;
            SetColors(SelectedColors);
        }

        public void Deselect()
        {
            Selected = false;
            SetColors(UnselectedColors);
        }

        public void Press()
        {
            SetColors(PressedColors);
            calledWhenPressed();
        }

        #endregion

        #endregion
    }
}
