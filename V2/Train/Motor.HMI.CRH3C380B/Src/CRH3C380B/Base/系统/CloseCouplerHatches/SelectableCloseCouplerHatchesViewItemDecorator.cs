using System.Drawing;
using CommonUtil.Controls;

namespace Motor.HMI.CRH3C380B.Base.系统.CloseCouplerHatches
{
    internal class SelectableCloseCouplerHatchesViewItemDecorator : CommonInnerControlBase, ICouplerHatchesStateItem
    {
        public CouplerHatchesViewItem CloseCouplerHatchesViewItemDecorater { get; private set; }

        private readonly Size m_SelectedInflate;

        private Region m_DrawRegion;

        public SelectableCloseCouplerHatchesViewItemDecorator(
            CouplerHatchesViewItem closeCouplerHatchesViewItemDecorater, Size selectInflate)
        {
            CloseCouplerHatchesViewItemDecorater = closeCouplerHatchesViewItemDecorater;
            Selected = false;
            SelectedBrush = Brushes.SkyBlue;
            m_SelectedInflate = selectInflate;

            UpdateOutline();

            UpdateDrawRegion();
        }


        public Brush SelectedBrush { set; get; }

        public bool Selected { set; get; }

        public CouplerHatchesUnit Unit
        {
            get { return CloseCouplerHatchesViewItemDecorater.Unit; }
        }

        public CouplerHatchesState State
        {
            get { return CloseCouplerHatchesViewItemDecorater.State; }
            set { CloseCouplerHatchesViewItemDecorater.State = value; }
        }

        private void UpdateOutline()
        {
            var bod = new Rectangle(CloseCouplerHatchesViewItemDecorater.Location,
                CloseCouplerHatchesViewItemDecorater.Size);
            bod.Inflate(m_SelectedInflate);
            m_OutLineRectangle = bod;
        }

        private void UpdateDrawRegion()
        {
            m_DrawRegion = new Region(m_OutLineRectangle);

            m_DrawRegion.Exclude(CloseCouplerHatchesViewItemDecorater.OutLineRectangle);
        }

        public override void OnDraw(Graphics g)
        {
            CloseCouplerHatchesViewItemDecorater.OnPaint(g);

            if (Selected)
            {
                g.FillRegion(SelectedBrush, m_DrawRegion);
            }
        }
    }
}
