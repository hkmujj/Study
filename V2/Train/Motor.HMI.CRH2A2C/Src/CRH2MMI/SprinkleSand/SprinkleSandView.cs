using System.Drawing;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View;
using CRH2MMI.Common.View.Train;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.SprinkleSand
{
    [GksDataType(DataType.isMMIObjectClass)]
    class SprinkleSandView : CRH2BaseClass
    {
        private TrainView m_TrainView;

        private SprinkleSandContentView m_SprinkleSandContentView;


        public override bool Init()
        {
            m_TrainView = TrainView.Instance;
            m_SprinkleSandContentView = new SprinkleSandContentView(this);
            m_SprinkleSandContentView.Init();
            return true;
        }

        public override void NavigateTo(ViewConfig toThis)
        {
            UpdateTrainView();
        }

        public void UpdateTrainView()
        {
            var location = GlobalInfo.CurrentCRH2Config.SprinkleSandConfig.TrainViewLocation.Rectangle.Location;
            m_TrainView.Location = location;
            m_TrainView.NeedDrawCarName = false;
            m_TrainView.IsUnitStateVisible = false;
            m_TrainView.IsDriverRoomVisible = true;
            m_TrainView.IsUnitNameVisible = false;
        }

        /// <summary>»æÖÆÍ¼Ïñ</summary>
        /// <param name="g"></param>
        public override void paint(Graphics g)
        {
            m_SprinkleSandContentView.OnPaint(g);
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_SprinkleSandContentView.OnMouseDown(point);
        }

        protected override bool OnMouseUp(Point point)
        {
            return m_SprinkleSandContentView.OnMouseUp(point);
        }
    }
}