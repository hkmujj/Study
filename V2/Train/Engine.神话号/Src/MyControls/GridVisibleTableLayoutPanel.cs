using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace CommonControls
{
    public partial class GridVisibleTableLayoutPanel : TableLayoutPanel
    {
        private Pen m_CellFramePen;
        private Color _borderColor;
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Browsable(true)]
        public Pen CellFramePen
        {
            set { m_CellFramePen = value; Invalidate(); }
            get { return m_CellFramePen; }
        }

        [Browsable(true)]
        public Color BorderColor
        {
            get { return _borderColor; }
            set { _borderColor = value; m_CellFramePen = new Pen(new SolidBrush(BorderColor)); Invalidate(); }
        }

        public GridVisibleTableLayoutPanel()
        {
            InitializeComponent();

            SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            CellFramePen = new Pen(new SolidBrush(BorderColor),0.5F);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.DrawRectangle(m_CellFramePen, e.ClipRectangle.X, e.ClipRectangle.Y, e.ClipRectangle.Width - 1, e.ClipRectangle.Height - 1);
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();

            this.CellBorderStyle = TableLayoutPanelCellBorderStyle.None;

            CellFramePen = new Pen(new SolidBrush(BorderColor), 0.5F);
        }

        protected override void OnCellPaint(TableLayoutCellPaintEventArgs e)
        {
            base.OnCellPaint(e);

            if (m_CellFramePen != null)
            {
                e.Graphics.DrawRectangle(m_CellFramePen, e.CellBounds.X, e.CellBounds.Y, e.CellBounds.Width, e.CellBounds.Height);
            }
        }
    }
}
