using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleLib.UI.Modules;

namespace ConsoleLib.UI.Components
{
    public class ButtonComponent : TextBoxComponent, IRenderable
    {
        public ButtonComponent(string Name, DrawableComponent drawInfo,
            IDrawableUnit SelectedColors, IDrawableUnit PressedColors,
            IDrawableUnit UnselectedColors)
            : base(Name,drawInfo)
        {
            this.SelectedColors = SelectedColors;
            this.UnselectedColors = UnselectedColors;
            this.PressedColors = PressedColors;

            Pressed = false;
            Selected = false;
            SetColors(UnselectedColors);
        }

        #region Functioning Loop
        public void Render()
        {
            base.Render();
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

        public void Select()
        {
            Selected = true;
            SetColors(SelectedColors);
        }

        public void Unselect()
        {
            Selected = false;
            SetColors(UnselectedColors);
        }

        public void Press()
        {
            Pressed = true;
            SetColors(PressedColors);
        }

        #endregion
    }
}
