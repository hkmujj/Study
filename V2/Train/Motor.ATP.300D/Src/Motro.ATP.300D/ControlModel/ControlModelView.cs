using System;
using System.Collections.Generic;
using System.Drawing;
using CommonUtil.Controls;
using Motor.ATP.Domain.Interface;

namespace Motor.ATP._300D.ControlModel
{
    /// <summary>
    /// 控制模式界面.
    /// </summary>
    public class ControlModelView : CommonInnerControlBase
    {
        private ControlType m_ControlType;

        private int m_ImageIndex;

        private readonly Font m_Font = new Font(new FontFamily("幼圆"), 15, FontStyle.Bold);

        private readonly StringFormat m_StringFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };

        private Action<Graphics> m_DrawAction;

        public Image[] Images { set; get; }

        public ControlType ControlType
        {
            set
            {
                m_ControlType = value;
                ResetDrawAction();
                switch (value)
                {
                    case ControlType.Unknown:
                        break;
                    case ControlType.FullSupervision:
                        break;
                    case ControlType.PartialSupervision:
                        break;
                    case ControlType.CallingOn:
                        break;
                    case ControlType.Shunting:
                        break;
                    case ControlType.OnSight:
                        break;
                    case ControlType.StandBy:
                        break;
                    case ControlType.LKJ:
                        break;
                    case ControlType.Trip:
                        break;
                    case ControlType.PostTrip:
                        break;
                    case ControlType.Overtaking:
                        m_DrawAction = g => g.DrawString("越行", m_Font, Brushes.White, OutLineRectangle, m_StringFormat);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException("value");
                }
            }
            get { return m_ControlType; }
        }

        public ControlModelView()
        {
            m_ImageIndex = -1;
            ResetDrawAction();
        }

        private void ResetDrawAction()
        {
            m_DrawAction = g => { };
        }


        public override void OnDraw(Graphics g)
        {
            m_DrawAction(g);
        }
    }
}
