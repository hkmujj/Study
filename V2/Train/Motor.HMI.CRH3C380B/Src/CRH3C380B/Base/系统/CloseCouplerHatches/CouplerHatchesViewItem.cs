using System;
using System.Drawing;
using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches
{
    public class CouplerHatchesViewItem : CommonInnerControlBase, ICouplerHatchesStateItem
    {
        private Action<Graphics> m_DrawContent;

        private CouplerHatchesState m_State;
        public CouplerHatchesUnit Unit { get; private set; }
        public Brush ContentBrush { set; get; }
        private readonly Pen m_OpenPen = new Pen(Brushes.Red, 2f);
        public CouplerHatchesViewItem(CouplerHatchesUnit unit = CouplerHatchesUnit.None, CouplerHatchesState state = CouplerHatchesState.Close)
        {
            Unit = unit;
            State = state;
            ContentBrush = Brushes.Yellow;
        }
        public CouplerHatchesState State
        {
            get { return m_State; }
            set
            {
                m_State = value;
                RefreshDrawContent();
            }
        }

        private void RefreshDrawContent()
        {
            if ((int)State == 4 || (int)State == 6)
            {
                return;
            }
            switch (State)
            {
                case CouplerHatchesState.Close:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Red, OutLineRectangle);
                    break;
                case CouplerHatchesState.Open:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.DrawEllipse(m_OpenPen, OutLineRectangle);
                    break;
                case CouplerHatchesState.Motion:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Red, OutLineRectangle);
                    m_DrawContent += graphics => graphics.FillRectangle(Brushes.Black, new Rectangle(OutLineRectangle.Location.X + 10, OutLineRectangle.Location.Y, OutLineRectangle.Width - 20, OutLineRectangle.Height));
                    break;
                case CouplerHatchesState.Coupled:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.FillRectangle(Brushes.White, new Rectangle(OutLineRectangle.Location.X + 10, OutLineRectangle.Location.Y, OutLineRectangle.Width - 20, OutLineRectangle.Height));
                    break;
                case CouplerHatchesState.PlanCoupl:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.DrawEllipse(m_OpenPen, OutLineRectangle);
                    m_DrawContent += graphics => graphics.FillEllipse(Brushes.White, new Rectangle(OutLineRectangle.Location.X + 3, OutLineRectangle.Location.Y + 3, OutLineRectangle.Width - 6, OutLineRectangle.Height - 6));
                    break;
                case CouplerHatchesState.Breakdown:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.DrawLine(m_OpenPen, OutLineRectangle.X + 3, OutLineRectangle.Y, OutLineRectangle.X + OutLineRectangle.Width - 3, OutLineRectangle.Y + OutLineRectangle.Height);
                    m_DrawContent += graphics => graphics.DrawLine(m_OpenPen, OutLineRectangle.X + 3, OutLineRectangle.Y+OutLineRectangle.Height, OutLineRectangle.X + OutLineRectangle.Width - 3, OutLineRectangle.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public override void OnDraw(Graphics g)
        {
            m_DrawContent(g);
        }
    }
}
