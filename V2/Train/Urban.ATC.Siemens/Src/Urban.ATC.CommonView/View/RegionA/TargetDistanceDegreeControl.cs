using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Windows.Forms;
using Motor.ATP.CommonView.Model;
using Motor.ATP.CommonView.Utils.Extensions;
using Motor.ATP.Domain.Interface.TargetDistance;

namespace Motor.ATP.CommonView.View.RegionA
{
    public partial class TargetDistanceDegreeControl : UserControl
    {
        private ITargitDistanceScale m_TargitDistanceScale;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public ITargitDistanceScale TargitDistanceScale
        {
            set
            {
                Contract.Requires(value != null);
                m_TargitDistanceScale = value;
                Invalidate();
            }
            get { return m_TargitDistanceScale; }
        }

        public TargetDistanceDegreeControl()
        {
            InitializeComponent();
            SetStyle(
                ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.OptimizedDoubleBuffer, true);

            TargitDistanceScale = new DefaultTargitDistanceScale();
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
            foreach (var item in TargitDistanceScale.TargitDistanceScaleItems)
            {
                var h = item.DegreeLocation * region.Height;
                var y = (float)(region.Height - h);
                g.DrawLine(item.DegreePen, (float)(region.Width * (1 - item.DegreeLength)), y, region.Width, y);
            }

            base.OnPaint(e);
        }
    }
}
