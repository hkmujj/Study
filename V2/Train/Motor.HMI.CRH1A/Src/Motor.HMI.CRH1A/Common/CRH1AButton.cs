using System.Drawing;
using CommonUtil.Controls.Button;
using Motor.HMI.CRH1A.Common.Global;

namespace Motor.HMI.CRH1A.Common
{
    public class CRH1AButton : GDIButton
    {
        public CRH1AButton()
        {
            GlobalInfo.Instance.ButtonStatusChange += () =>
            {
                IsEnable = GlobalInfo.Instance.ButtonEnable;
            };
        }
        public override bool OnMouseDown(Point point)
        {
            if (IsEnable)
            {
                return base.OnMouseDown(point);
            }
            else
            {
                return false;
            }
        }

        public override bool OnMouseUp(Point point)
        {
            if (IsEnable)
            {
                return base.OnMouseUp(point);
            }
            else
            {
                return false;
            }
        }
    }
}