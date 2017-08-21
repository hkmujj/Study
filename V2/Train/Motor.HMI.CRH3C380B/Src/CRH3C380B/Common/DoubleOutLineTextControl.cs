using System;
using System.Drawing;
using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Common
{
    internal class DoubleOutLineTextControl : GDIRectText
    {
        public Padding Padding
        {
            set
            {
                m_Padding = value;
                UpdateInnerRectangle();
            }
            get { return m_Padding; }
        }

        public bool InnerRectangleVisible { set; get; }

        private Rectangle m_InnerRectangle;
        private Padding m_Padding;

        public DoubleOutLineTextControl()
        {
            OutLineChanged += OnOutLineChanged;
        }

        private void OnOutLineChanged(object sender, EventArgs eventArgs)
        {
            UpdateInnerRectangle();
        }

        private void UpdateInnerRectangle()
        {
            m_InnerRectangle = new Rectangle(Location.X + Padding.Left, Location.Y + Padding.Top,
                Size.Width - Padding.Horizontal, Size.Height - Padding.Vertical);
        }

        public override void OnDraw(Graphics g)
        {
            base.OnDraw(g);

            if (InnerRectangleVisible)
            {
                g.DrawRectangle(OutLinePen, m_InnerRectangle);
            }
        }
    }
}
