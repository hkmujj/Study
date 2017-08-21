using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.AirConditioning
{
    internal class SelectableAirconditionViewItemDecorator : CommonInnerControlBase, IAirconditionStateItem, ISelectable
    {
        public AirconditionViewItem AirconditionViewItemDecorater { get; private set; }

        private readonly Size m_SelectedInflate;

        private Region m_DrawRegion;

        public SelectableAirconditionViewItemDecorator(AirconditionViewItem airconditionViewItemDecorater,
            Size selectInflate)
        {
            AirconditionViewItemDecorater = airconditionViewItemDecorater;
            Selected = false;
            SelectedBrush = Brushes.Blue;
            m_SelectedInflate = selectInflate;



            UpdateOutline();

            UpdateDrawRegion();
        }

        public Brush SelectedBrush { set; get; }

        public bool Selected { set; get; }

        public AirconditionLevelUnit Unit
        {
            get { return AirconditionViewItemDecorater.Unit; }
        }

        public AirConditionState State
        {
            get { return AirconditionViewItemDecorater.State; }
            set { AirconditionViewItemDecorater.State = value; }
        }

        private void UpdateOutline()
        {
            var bod = new Rectangle(AirconditionViewItemDecorater.Location, AirconditionViewItemDecorater.Size);
            bod.Inflate(m_SelectedInflate);
            m_OutLineRectangle = bod;
        }

        private void UpdateDrawRegion()
        {
            m_DrawRegion = new Region(m_OutLineRectangle);

            m_DrawRegion.Exclude(AirconditionViewItemDecorater.OutLineRectangle);
        }

        public override void OnDraw(Graphics g)
        {
            if (Selected)
            {
                g.FillRegion(SelectedBrush, m_DrawRegion);
            }
            AirconditionViewItemDecorater.OnPaint(g);
        }
    }
}