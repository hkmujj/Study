using System.Diagnostics;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Controls.Button;
using CommonUtil.Util;

namespace Motor.HMI.CRH1A.Common.View
{
    /// <summary>
    /// 使用本策略需要 down和 up 配对
    /// </summary>
    class CRH1ButtonStrategy : BtnBehavierNormalStrategy
    {
        [DebuggerStepThrough]
        public CRH1ButtonStrategy(CRH1AButton btn)
            : base(btn)
        {
        }

        public override bool OnMouseUp(Point point)
        {
            if (Control.CurrentMouseState == MouseState.MouseDown)
            {
                if (!base.OnMouseUp(point) )
                {
                    Control.OnButtonUp();
                }
                else
                {
                    HandleUtil.OnHandle(Control.ButtonClickEvent, Control, null);
                }
                return true;
            }
            return false;
        }
    }
}