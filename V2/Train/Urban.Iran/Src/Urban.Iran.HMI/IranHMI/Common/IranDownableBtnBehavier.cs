using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;

namespace Urban.Iran.HMI.Common
{
    internal class IranDownableBtnBehavier : BtnBehavierNormalStrategy
    {

        public IranDownableBtnBehavier(GDIButton btn)
            : base(btn)
        {
            btn.CurrentMouseStateChanged = (sender, args) =>
            {
                var b = (GDIButton) sender;
                switch (b.CurrentMouseState)
                {
                    case MouseState.MouseDown:
                        b.TextColor = Color.White;
                        break;
                    case MouseState.MouseUp:
                        b.TextColor = GdiCommon.MediumGreyPen.Color;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            };
        }

        public override void OnDraw(Graphics g)
        {
            if (!Control.Visible)
            {
                return;
            }

            Control.DrawBackImage(g);

            Control.DrawText(g);
        }
    }
}