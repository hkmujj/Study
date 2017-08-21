using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using CommonUtil.Model;
using Motor.ATP.CommonView.Utils.Extensions;

namespace Motor.ATP.CommonView.View.RegionA
{
    public partial class DistanceProgressBar : UserControl
    {
        private Direction m_Direction;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Brush ContentBrush
        {
            set { m_ContentBrush = value; Invalidate(); }
            get { return m_ContentBrush; }
        }

        private RectangleF m_ContentRectangleF;
        private double m_ScaleValue;
        private Brush m_ContentBrush;

        public Direction Direction
        {
            set
            {
                m_Direction = value;
                UpdateContentRegion();
                Invalidate();
            }
            get { return m_Direction; }
        }


        /// <summary>
        /// 当前值, 比例值
        /// </summary>
        public double ScaleValue
        {
            set
            {
                m_ScaleValue = value;
                UpdateContentRegion();
                Invalidate();
            }
            get { return m_ScaleValue; }
        }


        public DistanceProgressBar()
        {
            InitializeComponent();

            m_ContentRectangleF = RectangleF.Empty;

            ContentBrush = new SolidBrush(ForeColor);
            ForeColorChanged += (sender, args) =>
            {
                if (ContentBrush != null)
                {
                    ContentBrush.Dispose();
                }
                ContentBrush = new SolidBrush(ForeColor);
            };

            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            SizeChanged += (sender, args) => UpdateContentRegion();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (m_ContentRectangleF != RectangleF.Empty)
            {
                e.Graphics.FillRectangle(ContentBrush, m_ContentRectangleF);
            }

            base.OnPaint(e);
        }

        private void UpdateContentRegion()
        {
            var region = this.GetActureRectangleF();
            RectangleF tmp;

            switch (Direction)
            {
                case Direction.Up:
                    tmp = RectangleF.Inflate(region, 0, -(float)((1 - ScaleValue) * region.Height));
                    tmp.Height = (float)(region.Height * ScaleValue);
                    m_ContentRectangleF = tmp;
                    break;
                case Direction.Down:
                    tmp = region;
                    tmp.Height = (float)(region.Height * ScaleValue);
                    m_ContentRectangleF = tmp;
                    break;
                case Direction.Left:
                    tmp = RectangleF.Inflate(region, -(float)((1 - ScaleValue) * region.Width), 0);
                    tmp.Width = (float)(region.Width * ScaleValue);
                    m_ContentRectangleF = tmp;
                    break;
                case Direction.Right:
                    tmp = region;
                    tmp.Width = (float)(region.Width * ScaleValue);
                    m_ContentRectangleF = tmp;
                    break;
                default:
                    m_ContentRectangleF = RectangleF.Empty;
                    break;
            }
        }
    }
}
