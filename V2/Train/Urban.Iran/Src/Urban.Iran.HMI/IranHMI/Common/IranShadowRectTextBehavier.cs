using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Util.Extension;

namespace Urban.Iran.HMI.Common
{
    internal class IranShadowRectTextBehavier : DefaultRectTextBehavierStrategy
    {
        public Brush ShadowTextBrush { set; get; }


        // ReSharper disable once InconsistentNaming
        public Func<Point, Point> LocationTraslateAction;

        public IranShadowRectTextBehavier(GDIRectText txt)
            : base(txt)
        {
            LocationTraslateAction = point => point.Translate(3, 3);
        }

        public override void OnDraw(Graphics g)
        {
            if (!Control.Visible)
            {
                return;
            }

            Control.DrawBk(g);

            Control.DrawText(g);

            if (ShadowTextBrush != null)
            {
                var region = new Rectangle(LocationTraslateAction(Control.Location), Control.Size);
                g.DrawString(Control.Text, Control.DrawFont, ShadowTextBrush, region, Control.TextFormat);
            }
        }
    }
}