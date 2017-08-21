using System;
using System.Drawing;
using System.Windows.Forms;
using Motor.ATP.CommonView.Utils.Extensions;

namespace Motor.ATP.CommonView.View
{
    /// <summary>
    /// 区域文本
    /// </summary>
    public partial class RectanceText : Label
    {
        private Rectangle m_OutlineRegion;
        private bool m_OutlineSelected;
        private Pen m_OutlinePen;
        private Pen m_SelectedPen;

        public Pen OutlinePen
        {
            set { m_OutlinePen = value; Invalidate(); }
            get { return m_OutlinePen; }
        }

        public Pen SelectedPen
        {
            set { m_SelectedPen = value;Invalidate(); }
            get { return m_SelectedPen; }
        }

        public bool OutlineSelected
        {
            set
            {
                m_OutlineSelected = value; 
                Invalidate();
            }
            get { return m_OutlineSelected; }
        }

        public RectanceText()
        {
            OutlinePen = Pens.Red;
            OnRegionChanged(null, null);
            InitializeComponent();
            LocationChanged += OnRegionChanged;
            SizeChanged += OnRegionChanged;
        }

        private void OnRegionChanged(object sender, EventArgs eventArgs)
        {
            m_OutlineRegion = Rectangle.Inflate(this.GetActureRectangle(), -2, -2);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            var g = e.Graphics;
            if (OutlinePen != null)
            {
                g.DrawRectangle(OutlinePen, this.GetActureRectangle());
            }
            if (OutlineSelected && SelectedPen != null)
            {
                g.DrawRectangle(SelectedPen, m_OutlineRegion);
            }
        }
    }
}
