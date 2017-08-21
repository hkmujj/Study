using Motor.ATP.CommonView.Utils.Extensions;
using System;
using System.Drawing;
using System.Windows.Forms;
using Urban.ATC.CommonView.Model;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class TestControl : UserControl
    {
        private InfoLevl m_Level;
        private readonly Pen m_LinePen;
        private SolidBrush m_SolidBrush;
        private string m_Content;
        private Rectangle m_Rectangle;

        public string Content
        {
            get { return m_Content; }
            set
            {
                if (m_Content != null && m_Content == value)
                {
                    return;
                }
                m_Content = value;
                Invalidate();
            }
        }

        public InfoLevl Level
        {
            get { return m_Level; }
            set
            {
                if (m_Level == value)
                {
                    return;
                }
                m_Level = value;
                UpdateFramColor();
                Invalidate();
            }
        }

        private void UpdateFramColor()
        {
            switch (m_Level)
            {
                case InfoLevl.Yellow:
                    m_SolidBrush = GDICommon.YellowBrush;
                    break;

                case InfoLevl.Red:
                    m_SolidBrush = GDICommon.RedBrush;
                    break;

                case InfoLevl.Green:
                    m_SolidBrush = GDICommon.LightGreenBrush;
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            m_LinePen.Color = m_SolidBrush.Color;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(label1.Text))
            {
                base.OnPaint(e);
                return;
            }
            // e.Graphics.FillRectangle(m_SolidBrush, m_Rectangle);

            e.Graphics.DrawRectangle(m_LinePen, m_Rectangle);
            base.OnPaint(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            m_Rectangle = Rectangle.Inflate(this.GetActureRectangle(), -2, -2);

            base.OnSizeChanged(e);
        }

        public TestControl()
        {
            InitializeComponent();

            m_Timer.Start();
            this.flowLayoutPanel1.BackColor = Color.Transparent;
            label1.Text = @"确认释放速度以低速接近";
            label2.Text = @"confirm Release speed";
            label1.BackColor = Color.Transparent;
            m_LinePen = new Pen(GDICommon.YellowBrush, 2f);
            m_Rectangle = new Rectangle(Location.X + 2, Location.Y + 2, Width - 2 * 2, Height - 2 * 2);
        }

        private void m_Timer_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}