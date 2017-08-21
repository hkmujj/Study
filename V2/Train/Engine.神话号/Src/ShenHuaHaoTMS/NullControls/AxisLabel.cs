using System.Drawing;
using System.Windows.Forms;

namespace ShenHuaHaoTMS.NullControls
{
    public partial class AxisLabel : UserControl
    {
        public AxisLabel()
        {
            InitializeComponent();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

            e.Graphics.DrawLine(
                _pen,
                new Point(0,0),
                new Point(Size)
                );

            //e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.Default;

            e.Graphics.DrawString(
                "轴",
                Font,
                Brushes.White,
                new Rectangle(0+3,0,this.Size.Width,this.Size.Height),
                _sf1
                );

            e.Graphics.DrawString(
                "位",
                Font,
                Brushes.White,
                new Rectangle(0, 0, this.Size.Width-5, this.Size.Height),
                _sf2
                );
        }

        private StringFormat _sf1 = new StringFormat()
        {
            Alignment = StringAlignment.Near,
            LineAlignment = StringAlignment.Far
        };

        private StringFormat _sf2 = new StringFormat()
        {
            Alignment = StringAlignment.Far,
            LineAlignment = StringAlignment.Near
        };

        private Pen _pen = new Pen(Brushes.White, 2);
    }
}
