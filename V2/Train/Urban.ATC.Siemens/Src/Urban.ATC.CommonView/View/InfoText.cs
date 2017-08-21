using System;
using System.Drawing;
using System.Windows.Forms;
using Urban.ATC.CommonView.Model;
using Urban.ATC.Domain.Interface.ViewStates;

namespace Urban.ATC.CommonView.View
{
    public partial class InfoText : TextBase
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
                ChangeText(m_Content);
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

        public InfoText()
        {
            InitializeComponent();
            m_LinePen = new Pen(GDICommon.YellowBrush, 2f);
            m_Rectangle = new Rectangle(this.Location.X + 2, this.Location.Y + 2, this.Width - 2 * 2, this.Height - 2 * 2);
            timer1.Start();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            m_Rectangle = new Rectangle(0, 0, Width, Height);

            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (string.IsNullOrEmpty(GetText()))
            {
                base.OnPaint(e);
                return;
            }
            e.Graphics.DrawRectangle(m_LinePen, m_Rectangle);
            // e.Graphics.FillRectangle(m_SolidBrush, m_Rectangle);
            base.OnPaint(e);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Invalidate();
        }
    }
}