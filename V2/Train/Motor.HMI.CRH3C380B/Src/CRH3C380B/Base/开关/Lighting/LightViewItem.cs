using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Base.开关.Lighting
{
    public class LightViewItem : CommonInnerControlBase, ILightStateItem
    {
        private readonly Pen m_OutLinePen;

        private Action<Graphics> m_DrawContent;

        /// <summary>
        /// 右下角三角形
        /// </summary>
        private GraphicsPath m_TrianglePath;

        private LightState m_State;

        public LightViewItem(LightingUnit unit = LightingUnit.None, LightState state = LightState.Light0)
        {
            Unit = unit;
            State = state;
            m_OutLinePen = Pens.White;
            ContentBrush = Brushes.Yellow;
            OutLineChanged += (sender, args) => RefreshTrianglePath();
        }

        public Brush ContentBrush { set; get; }

        public LightingUnit Unit { get; private set; }

        public LightState State
        {
            get { return m_State; }
            set
            {
                m_State = value;
                RefreshDrawContent();
            }
        }

        private void RefreshTrianglePath()
        {
            m_TrianglePath = new GraphicsPath();
            m_TrianglePath.AddLine(OutLineRectangle.Left, OutLineRectangle.Bottom, OutLineRectangle.Right,
                OutLineRectangle.Top);
            m_TrianglePath.AddLine(OutLineRectangle.Right, OutLineRectangle.Top, OutLineRectangle.Right,
                OutLineRectangle.Bottom);
            m_TrianglePath.AddLine(OutLineRectangle.Right, OutLineRectangle.Bottom, OutLineRectangle.Left,
                OutLineRectangle.Bottom);
        }

        private void RefreshDrawContent()
        {
            switch (State)
            {
                case LightState.Light0:
                    m_DrawContent = graphics => { };
                    break;
                case LightState.Light1P3:
                    m_DrawContent = graphics => graphics.FillPath(ContentBrush, m_TrianglePath);
                    break;
                case LightState.Light1:
                    m_DrawContent = graphics => graphics.FillRectangle(ContentBrush, OutLineRectangle);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public override void OnDraw(Graphics g)
        {
            g.DrawRectangle(m_OutLinePen, OutLineRectangle);

            m_DrawContent(g);
        }
    }
}
