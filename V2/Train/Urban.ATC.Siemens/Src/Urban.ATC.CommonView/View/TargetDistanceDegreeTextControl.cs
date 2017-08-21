using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.CommonView.View.RegionA;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Urban.ATC.CommonView.View
{
    internal partial class TargetDistanceDegreeTextControl : TargetDistanceDegreeControl
    {
        private float m_TextPercent;

        private readonly StringFormat m_DegreeTextFormat = new StringFormat()
        {
            LineAlignment = StringAlignment.Center,
            Alignment = StringAlignment.Far
        };

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Font DegreeTextFont { set; get; }

        public float TextPercent
        {
            set { m_TextPercent = value; Invalidate(); }
            get { return m_TextPercent; }
        }

        public TargetDistanceDegreeTextControl()
        {
            InitializeComponent();
            TextPercent = 0.8f;
            DegreeTextFont = new Font("宋体", 9);

            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            var g = e.Graphics;

            if (TargitDistanceScale == null)
            {
                base.OnPaint(e);
                return;
            }
            var region = this.GetActureRectangle();
            var top = 10;
            region = new Rectangle(region.X, region.Y + top, region.Width, region.Height - top);
            foreach (var item in TargitDistanceScale.TargitDistanceScaleItems)
            {
                var h = item.DegreeLocation * region.Height;
                var y = (float)(region.Height - h + region.Y);
                var startf = TextPercent + (1 - TextPercent) * (1 - item.DegreeLength);
                g.DrawLine(item.DegreePen, (float)(region.Width * startf), y, region.Width, y);
                g.DrawString(item.Text, DegreeTextFont, Brushes.LightGray,
                    new RectangleF(0, y - 10, region.Width * TextPercent, 20),
                    m_DegreeTextFormat);
            }

            //base.OnPaint(e);
        }
    }
}