using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace Motor.ATP.CommonView.View
{
    public partial class GridVisibleTableLayoutPanel : TableLayoutPanel
    {
        private Pen m_CellFramePen;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Browsable(false)]
        public Pen CellFramePen
        {
            set { m_CellFramePen = value; Invalidate(); }
            get { return m_CellFramePen; }
        }

        public GridVisibleTableLayoutPanel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
        }

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            base.OnCellPaint(e);

            if (CellFramePen != null)
            {
                e.Graphics.DrawRectangle(CellFramePen, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width , e.CellBounds.Height );
            }
        }
    }
}
