using System.Drawing;
using CommonUtil.Controls.Button;

namespace Urban.Iran.HMI.Common
{
    internal class IranHmiDefaultButtonBehavier : BtnBehavierNormalStrategy
    {
        public IranHmiDefaultButtonBehavier(GDIButton control)
            : base(control)
        {
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

    internal class IranImageButtonBehavier : BtnBehavierNormalStrategy
    {
        public IranImageButtonBehavier(GDIButton btn)
            : base(btn)
        {
        }

        public override void OnDraw(Graphics g)
        {
            if (!Control.Visible)
            {
                return;
            }

            if (Control.BackImage != null)
            {
                g.DrawImage(Control.BackImage, Control.OutLineRectangle);
            }
        }
    }
}