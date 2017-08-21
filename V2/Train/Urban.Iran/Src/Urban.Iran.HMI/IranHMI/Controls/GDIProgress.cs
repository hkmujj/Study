using System;
using System.Drawing;
using CommonUtil.Controls;
using CommonUtil.Model;

namespace Urban.Iran.HMI.Controls
{
    public class GDIProgress : CommonInnerControlBase
    {
        public Direction Direction { get; private set; }
        private Color m_BackgroundColor;
        private Brush m_Brush;

        private Rectangle m_OutLineRectangle;

        public float CurrentValue { get; set; }

        public float MaxValue { get; set; }

        public GDIProgress(Direction dPara)
        {
            Direction = dPara;
            m_Brush = new SolidBrush(Color.Black);
            OutLinePen = Pens.White;
            OutLineChanged += (sender, arg) =>
            {
                m_OutLineRectangle = new Rectangle(Location, Size);
            };

        }

        public Color BackgroundColor
        {
            get { return m_BackgroundColor; }
            set
            {
                if (m_BackgroundColor == value)
                {
                    return;
                }
                m_BackgroundColor = value;
                m_Brush = new SolidBrush(m_BackgroundColor);
            }
        }

        void RefreshValue()
        {
            switch (Direction)
            {
                case Direction.Up:
                    m_OutLineRectangle.Y = (int)(Location.Y + Size.Height * (1 - CurrentValue / MaxValue));
                    m_OutLineRectangle.Height = (int)(Size.Height * CurrentValue / MaxValue);
                    break;
                case Direction.Down:
                    m_OutLineRectangle.Height = (int)(Size.Height * CurrentValue / MaxValue);
                    break;
                case Direction.Left:
                    break;
                case Direction.Right:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        public Pen OutLinePen { get; set; }

        public bool NeedOutLine { get; set; }

        public override void OnDraw(Graphics g)
        {
            RefreshValue();
            if (!Visible) return;
            g.FillRectangle(m_Brush, m_OutLineRectangle);
            if (NeedOutLine)
            {
                g.DrawRectangle(OutLinePen, OutLineRectangle);
            }
        }
    }
}
