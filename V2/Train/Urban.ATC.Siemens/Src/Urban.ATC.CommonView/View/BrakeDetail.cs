using Motor.ATP.CommonView.Utils.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;
using Urban.ATC.CommonView.Model;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class BrakeDetail : UserControl
    {
        private BrakeDetailsType m_Type;
        private SolidBrush m_GraphiceBrush;
        private Rectangle m_Rectangle;
        private int m_Interval;

        public int Interval
        {
            get { return m_Interval; }
            set
            {
                if (m_Interval == value)
                {
                    return;
                }
                m_Interval = value;
                Invalidate();
            }
        }

        public BrakeDetailsType Type
        {
            get { return m_Type; }
            set
            {
                if (m_Type == value)
                {
                }
                m_Type = value;
                ChangeColor();
                Invalidate();
            }
        }

        private void ChangeColor()
        {
            switch (Type)
            {
                case BrakeDetailsType.Initial:
                    m_GraphiceBrush = GDICommon.BacgroundBrush;
                    break;

                case BrakeDetailsType.BrakingRequired:
                    m_GraphiceBrush = GDICommon.OrangeBrush;
                    break;

                case BrakeDetailsType.EnmergencyBrake:
                    m_GraphiceBrush = GDICommon.RedBrush;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public BrakeDetail()
            : this(BrakeDetailsType.Initial, 0)
        {
        }

        public BrakeDetail(BrakeDetailsType type, int inva)
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);

            Type = type;

            Interval = inva;
            m_Rectangle = new Rectangle(this.Location.X + Interval, this.Location.Y + Interval, this.Width - 2 * Interval, this.Height - 2 * Interval);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            m_Rectangle = Rectangle.Inflate(this.GetActureRectangle(), -Interval, -Interval);

            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(GDICommon.BacgroundBrush, this.GetActureRectangle());
            e.Graphics.FillRectangle(m_GraphiceBrush, m_Rectangle);
            base.OnPaint(e);
        }
    }
}