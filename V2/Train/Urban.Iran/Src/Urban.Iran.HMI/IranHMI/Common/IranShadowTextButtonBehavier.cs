using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util.Extension;

namespace Urban.Iran.HMI.Common
{
    internal class IranShadowTextButtonBehavier : BtnBehavierNormalStrategy
    {
        public Brush ShadowTextBrush { set; get; }


        // ReSharper disable once InconsistentNaming
        public Func<Point, Point> LocationTraslateAction;

        public IranShadowTextButtonBehavier(GDIButton btn)
            : base(btn)
        {
            LocationTraslateAction = point => point.Translate(3, 3);
        }

        public override void OnDraw(Graphics g)
        {
            if (!Control.Visible)
            {
                return;
            }

            Control.DrawBackImage(g);

            Control.DrawText(g);

            if (ShadowTextBrush != null)
            {
                var txt = (GDIRectText)Control.InnnerControl;
                var region = new Rectangle(LocationTraslateAction(txt.Location), txt.Size);
                g.DrawString(Control.Text, txt.DrawFont, ShadowTextBrush, region, txt.TextFormat);
            }
        }
    }
}