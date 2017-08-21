using System;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;
using Motor.ATP.Domain.Model.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionD
{
    /// <summary>
    /// 计划区坐标系
    /// </summary>
    public partial class PlanSectionCoordinatePanel : Panel
    {
        private IPlanSectionCoordinate m_PlanSectionCoordinate;
        private Bitmap m_ContentImage;

        private readonly object m_NeedInvalidateToken = new object();

        private bool m_NeedInvalidate;

        private bool NeedInvalidate
        {
            get { return m_NeedInvalidate; }
            set
            {
                lock (m_NeedInvalidateToken)
                {
                    m_NeedInvalidate = value;
                }
            }
        }

        public IPlanSectionCoordinate PlanSectionCoordinate
        {
            set
            {
                m_PlanSectionCoordinate = value;
                NeedInvalidate = true;
                UpdateAxsiLineBuff();
                Invalidate();
            }
            get { return m_PlanSectionCoordinate; }
        }

        public PlanSectionCoordinatePanel()
        {
            InitializeComponent();
            PlanSectionCoordinate = new DefaultPlanSectionCoordinate();
            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            UpdateContentImageSize();
            UpdateAxsiLineBuff();
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            UpdateContentImageSize();

            UpdateAxsiLineBuff();
        }

        private void UpdateContentImageSize()
        {
            if (m_ContentImage != null)
            {
                m_ContentImage.Dispose();
            }
            m_ContentImage = new Bitmap(this.GetActureRectangle().Width, this.GetActureRectangle().Height);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (PlanSectionCoordinate == null)
            {
                return;
            }

            var g = e.Graphics;

            g.DrawImage(m_ContentImage, this.GetActureRectangle());
        }

        protected override void NotifyInvalidate(Rectangle invalidatedArea)
        {
            if (NeedInvalidate)
            {
                NeedInvalidate = false;
                base.NotifyInvalidate(invalidatedArea);
            }
        }

        private void UpdateAxsiLineBuff()
        {
            var g = Graphics.FromImage(m_ContentImage);

            g.Clear(Color.Transparent);

            if (PlanSectionCoordinate != null)
            {
                DrawAxisLine(g);
            }

            m_ContentImage.MakeTransparent();

            g.Dispose();
        }

        private void DrawAxisLine(Graphics g)
        {
            var region = new RectangleF(PointF.Empty, m_ContentImage.Size);

            foreach (var xAxise in PlanSectionCoordinate.XAxises.Where(w => w.AxisPen != null))
            {
                var x = xAxise.LocationPercent * region.Width;
                var ts = g.MeasureString(xAxise.Text, xAxise.TextFont);
                g.DrawLine(xAxise.AxisPen, x, xAxise.Padding.Top * region.Height, x,
                    (1 - xAxise.Padding.Bottom) * region.Height - ts.Height);

                var textX = Math.Min(Width - ts.Width, Math.Max(0, x - ts.Width / 2));

                g.DrawString(xAxise.Text, xAxise.TextFont, xAxise.TextBrush, textX, Height - ts.Height);
            }

            foreach (var yAxise in PlanSectionCoordinate.YAxises.Where(w => w.AxisPen != null))
            {
                var y = (1 - yAxise.LocationPercent) * region.Height;
                g.DrawLine(yAxise.AxisPen, 0, y, region.Width, y);
                if (!string.IsNullOrWhiteSpace(yAxise.Text))
                {
                    var ts = g.MeasureString(yAxise.Text, yAxise.TextFont);
                    g.DrawString(yAxise.Text, yAxise.TextFont, yAxise.TextBrush, 0f, y - ts.Height);
                }
            }
        }
    }
}
