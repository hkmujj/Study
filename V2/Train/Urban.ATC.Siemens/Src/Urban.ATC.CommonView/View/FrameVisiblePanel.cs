using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;

namespace Motor.ATP.CommonView.View
{
    /// <summary>
    /// 边框可见的 panel
    /// </summary>
    public partial class FrameVisiblePanel : Panel
    {
        private Pen m_FramePen;

        public Pen FramePen
        {
            set { m_FramePen = value; Invalidate(); }
            get { return m_FramePen; }
        }

        public FrameVisiblePanel()
        {
            InitializeComponent();
            SetStyle(ControlStyles.ResizeRedraw | ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (FramePen != null)
            {
                e.Graphics.DrawRectangle(FramePen, this.GetRectangleWithoutPadding());
            }
        }
    }
}
