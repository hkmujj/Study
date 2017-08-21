using System;
using System.Drawing;
using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches
{
    class CloseCouplerHatchesViewItem : CommonInnerControlBase, ICloseCouplerHatchesStateItem
    {
        private Action<Graphics> m_DrawContent;

        private CloseCouplerHatchesState m_State;
        public CloseCouplerHatchesUnit Unit { get; private set; }
        public Brush ContentBrush { set; get; }
        private readonly Pen OpenPen = new Pen(Brushes.Red, 2f);
        public CloseCouplerHatchesViewItem(CloseCouplerHatchesUnit unit = CloseCouplerHatchesUnit.None, CloseCouplerHatchesState state = CloseCouplerHatchesState.Close)
        {
            Unit = unit;
            State = state;
            ContentBrush = Brushes.Yellow;
        }
        public CloseCouplerHatchesState State
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
            switch (State)
            {
                case CloseCouplerHatchesState.Close:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Red, OutLineRectangle);
                    break;
                case CloseCouplerHatchesState.Open:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.DrawEllipse(OpenPen, OutLineRectangle);
                    break;
                case CloseCouplerHatchesState.Motion:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Red, OutLineRectangle);
                    m_DrawContent += graphics => graphics.FillRectangle(Brushes.Black, new Rectangle(OutLineRectangle.Location.X + 10, OutLineRectangle.Location.Y, OutLineRectangle.Width - 20, OutLineRectangle.Height));
                    break;
                case CloseCouplerHatchesState.Coupled:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.FillRectangle(Brushes.White, new Rectangle(OutLineRectangle.Location.X + 10, OutLineRectangle.Location.Y, OutLineRectangle.Width - 20, OutLineRectangle.Height));
                    break;
                case CloseCouplerHatchesState.PlanCoupl:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.DrawEllipse(OpenPen, OutLineRectangle);
                    m_DrawContent += graphics => graphics.FillEllipse(Brushes.White, new Rectangle(OutLineRectangle.Location.X + 3, OutLineRectangle.Location.Y + 3, OutLineRectangle.Width - 6, OutLineRectangle.Height - 6));
                    break;
                case CloseCouplerHatchesState.Breakdown:
                    m_DrawContent = graphics => graphics.FillEllipse(Brushes.Black, OutLineRectangle);
                    m_DrawContent += graphics => graphics.DrawLine(OpenPen, OutLineRectangle.X + 3, OutLineRectangle.Y, OutLineRectangle.X + OutLineRectangle.Width - 3, OutLineRectangle.Y + OutLineRectangle.Height);
                    m_DrawContent += graphics => graphics.DrawLine(OpenPen, OutLineRectangle.X + 3, OutLineRectangle.Y+OutLineRectangle.Height, OutLineRectangle.X + OutLineRectangle.Width - 3, OutLineRectangle.Y);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public override void OnDraw(Graphics g)
        {
            //g.DrawRectangle(m_OutLinePen, OutLineRectangle);

            m_DrawContent(g);
        }
    }
}
