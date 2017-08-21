using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.SpeedMonitoringSection;
using Motor.ATP.Domain.Model.SpeedMonitoringSection;

namespace Motor.ATP.CommonView.View.RegionB
{
    /// <summary>
    /// 速度表盘刻度
    /// </summary>
    public partial class SpeedDialPlatePanel : Panel
    {
        private ISpeedDialPlate m_SpeedDialPlate;
        private Pen m_DegreePen;
        private Font m_DegreeDescriptionFont;
        private Brush m_DegreeDescriptionBrush;

        private readonly StringFormat m_DegreeTxtFormat;
        private Padding m_DialPlateMargins;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ISpeedDialPlate SpeedDialPlate
        {
            set
            {
                m_SpeedDialPlate = value;
                Invalidate();
            }
            get { return m_SpeedDialPlate; }
        }

        public Padding DialPlateMargins
        {
            set { m_DialPlateMargins = value; Invalidate(); }
            get { return m_DialPlateMargins; }
        }

        /// <summary>
        /// 默认白色
        /// </summary>
        public Pen DegreePen
        {
            set
            {
                m_DegreePen = value ?? Pens.White;
                Invalidate();
            }
            get { return m_DegreePen; }
        }

        public Font DegreeDescriptionFont
        {
            set { m_DegreeDescriptionFont = value ?? DefaultFont; Invalidate(); }
            get { return m_DegreeDescriptionFont; }
        }

        public Brush DegreeDescriptionBrush
        {
            set { m_DegreeDescriptionBrush = value ?? Brushes.White; Invalidate(); }
            get { return m_DegreeDescriptionBrush; }
        }

        public SpeedDialPlatePanel()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.DoubleBuffer | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
            m_DegreeTxtFormat = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
            SpeedDialPlate = new DefaultSpeedDialPlate();
            DegreePen = new Pen(Brushes.White, 2);
            DegreeDescriptionBrush = Brushes.White; 
            this.Resize += (sender, args) =>
            {
                Debug.Print("resize.ing.");
                SuspendLayout();
            };
            SizeChanged += (sender, args) =>
            {
                Debug.Print("SizeChanged");
                ResumeLayout(false);
            };
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;
            var old = g.Transform;
            if (m_SpeedDialPlate == null)
            {
                return;
            }

            e.Graphics.SmoothingMode = SmoothingMode.HighQuality;
            var region = this.GetActureRectangleF();
            region = new RectangleF(region.Left + DialPlateMargins.Left, region.Top + DialPlateMargins.Top,
                region.Width - DialPlateMargins.Size.Width
                , region.Height - DialPlateMargins.Size.Height);
            var center = new PointF(region.Width / 2 + region.Left, region.Height / 2 + region.Top);

            foreach (var degree in m_SpeedDialPlate.AllSpeedDegrees)
            {
                var points = new[]
                {new PointF(region.Left, center.Y), new PointF(degree.Lenght + region.Left, center.Y),};
                var mat = new Matrix();
                mat.RotateAt(degree.Angle, center);

                g.Transform = mat;
                g.DrawLine(m_DegreePen, points[0], points[1]);

                if (!string.IsNullOrWhiteSpace(degree.Text))
                {
                    var txtSize = g.MeasureString("0000", DegreeDescriptionFont);
                    var labCenter = new PointF(points[1].X + txtSize.Width / 2, points[1].Y );
                    var labMatrix = new Matrix();
                    labMatrix.RotateAt(-degree.Angle, labCenter);
                    mat.Multiply(labMatrix, MatrixOrder.Prepend);
                    g.Transform = mat;
                    g.DrawString(degree.Text,
                        DegreeDescriptionFont,
                        DegreeDescriptionBrush,
                        new RectangleF(new PointF(points[1].X, points[1].Y - txtSize.Height / 2), txtSize),
                        m_DegreeTxtFormat);
                }

                g.Transform = old;
        
            }
        }
    }
}
