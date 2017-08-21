using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Model;
using Motor.ATP.CommonView.Utils.Extensions;

namespace Motor.ATP.CommonView.View
{
    public partial class ProgressPanel : Panel
    {
        private Direction m_Direction;

        private readonly SolidBrush m_ForeBrush;
        private RectangleF m_ForeRectangleF;
        private float m_Value;

        [DefaultValue(Direction.Left)]
        public Direction Direction
        {
            set
            {
                m_Direction = value;
                UpdateForeRegion();
                Invalidate();
            }
            get { return m_Direction; }
        }

        public float Value
        {
            set
            {
                m_Value = value;
                UpdateForeRegion();
                Invalidate();
            }
            get { return m_Value; }
        }

        public ProgressPanel()
        {
            InitializeComponent();

            SetStyle(
                ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer |
                ControlStyles.UserMouse | ControlStyles.ResizeRedraw, true);
            Direction = Direction.Left;
            m_ForeBrush = new SolidBrush(ForeColor);
            m_ForeRectangleF = RectangleF.Empty;
        }

        protected override void OnForeColorChanged(EventArgs e)
        {
            m_ForeBrush.Color = ForeColor;

            base.OnForeColorChanged(e);
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            UpdateForeRegion();

            base.OnSizeChanged(e);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(m_ForeBrush, m_ForeRectangleF);
        }

        private void UpdateForeRegion()
        {
            float legth;
            float height;
            float width;
            float x;
            float y;
            var region = this.GetActureRectangleF();
            switch (Direction)
            {
                case Direction.Up:
                    legth = region.Height * Value/100;
                    height = region.Height - legth;
                    width = region.Width;
                    x = region.Left;
                    y = region.Bottom;
                    break;
                case Direction.Down:
                    legth = region.Height * Value / 100;
                    height = legth;
                    width = region.Width;
                    x = region.Left;
                    y = region.Top;
                    break;
                case Direction.Left:
                    legth = region.Width * Value / 100;
                    height = region.Height;
                    width = legth;
                    x = region.Left;
                    y = region.Top;
                    break;
                case Direction.Right:
                    legth = region.Width * Value / 100;
                    height = region.Height;
                    width = region.Width - legth;
                    x = region.Right;
                    y = region.Top;
                    break;
                default:
                    x = y = width = height = 0;
                    break;
            }
            m_ForeRectangleF= new RectangleF(x, y, width, height);
        }
    }
}
