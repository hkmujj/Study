using System.Drawing;
using CommonUtil.Controls;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.开关.Lighting
{
    public class SelectableLightViewItemDecorator : CommonInnerControlBase, ILightStateItem, ISelectable
    {
        public LightViewItem LightViewItemDecorater { get; private set; }

        private readonly Size m_SelectedInflate;

        private Region m_DrawRegion;

        public SelectableLightViewItemDecorator(LightViewItem lightViewItemDecorater, Size selectInflate)
        {
            LightViewItemDecorater = lightViewItemDecorater;
            Selected = false;
            SelectedBrush = Brushes.Blue;
            m_SelectedInflate = selectInflate;
            UpdateOutline();

            UpdateDrawRegion();
        }


        public Brush SelectedBrush { set; get; }

        public bool Selected { set; get; }

        public LightingUnit Unit { get { return LightViewItemDecorater.Unit; } }

        public LightState State { get { return LightViewItemDecorater.State; } set { LightViewItemDecorater.State = value; }}

        private void UpdateOutline()
        {
            var bod = new Rectangle(LightViewItemDecorater.Location, LightViewItemDecorater.Size);
            bod.Inflate(m_SelectedInflate);
            m_OutLineRectangle = bod;
        }

        private void UpdateDrawRegion()
        {
            m_DrawRegion = new Region(m_OutLineRectangle);

            m_DrawRegion.Exclude(LightViewItemDecorater.OutLineRectangle);
        }

        public override void OnDraw(Graphics g)
        {
            if (Selected)
            {
                g.FillRegion(SelectedBrush, m_DrawRegion);
            }

            LightViewItemDecorater.OnPaint(g);

        }
    }
}