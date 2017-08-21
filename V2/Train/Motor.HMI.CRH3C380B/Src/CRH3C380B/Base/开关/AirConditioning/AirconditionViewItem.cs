using System;
using System.Drawing;
using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Base.开关.AirConditioning
{
    internal class AirconditionViewItem : CommonInnerControlBase, IAirconditionStateItem
    {
        private readonly Pen m_OutLinePen;

        private Action<Graphics> m_DrawContent;

        private AirConditionState m_State;

        public AirconditionViewItem(AirconditionLevelUnit unit = AirconditionLevelUnit.None,
            AirConditionState state = AirConditionState.Default)
        {
            //AirconditionLevelUnit温度等级unit
            Unit = unit;
            State = state;
            m_OutLinePen = Pens.White;
            ContentBrush = Brushes.Yellow;
        }

        public Brush ContentBrush { set; get; }

        public AirconditionLevelUnit Unit { get; private set; }

        public AirConditionState State
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
                case AirConditionState.Default:
                    m_DrawContent = graphics => { };
                    //m_DrawContent = graphics => graphics.DrawRectangle(Pens.White,OutLineRectangle);
                    break;
                case AirConditionState.ManualOpen:
                case AirConditionState.Open:
                    m_DrawContent = graphics => graphics.FillRectangle(ContentBrush, OutLineRectangle);
                    break;
                case AirConditionState.EmergaceClose:
                    m_DrawContent = graphics => graphics.FillRectangle(Brushes.Red, OutLineRectangle);
                    break;
                case AirConditionState.SetTemperature:
                    m_DrawContent = graphics => graphics.FillRectangle(ContentBrush, OutLineRectangle);
                    m_DrawContent +=
                        graphics =>
                            graphics.DrawLine(new Pen(Color.Fuchsia, 2f),
                                new Point(OutLineRectangle.Location.X,
                                    OutLineRectangle.Location.Y + OutLineRectangle.Size.Height/2),
                                new Point(OutLineRectangle.Location.X + OutLineRectangle.Size.Width,
                                    OutLineRectangle.Location.Y + OutLineRectangle.Size.Height/2));
                    break;
                case AirConditionState.AutoOpen:
                    m_DrawContent = graphics => graphics.FillRectangle(Brushes.Green, OutLineRectangle);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnDraw(Graphics g)
        {
            m_DrawContent(g);
            g.DrawRectangle(m_OutLinePen, OutLineRectangle);
        }
    }
}
