using System.Drawing;
using System.Linq;
using CommonUtil.Controls;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Resource.Images;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.CutState
{
    /// <summary>
    /// �г�״̬
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class RemovalState : CRH2BaseClass
    {

        private RemovalStateContentViewBase m_ContentView;

        private CutResource m_Resource;

        private TrainView m_TrainView;

        private RectangleImage m_BottomHint;

        public override void paint(Graphics g)
        {
            OnDraw(g);
        }

        public override bool Init()
        {
            m_TrainView = TrainView.Instance;
            m_Resource = new CutResource(this);

            InitDate();

            var conf = GlobalInfo.CurrentCRH2Config.CutInfoConfig;
            conf.GridLine.ForEach(e => e.RefreshRelation());

            InitUIObj(conf.GridLine[0].Rows.SelectMany(s => s.ColumnConfigs));

            if (conf.GridLine.Sum(s => s.ColumnCount) > 8)
            {
                if (conf.GridLine.Count == 1)
                {
                    m_ContentView = new RemovalStateContentView2Acc();
                }
                else
                {
                    m_ContentView = new RemovalStateContentView1Acc();
                }
            }
            else
            {
                m_ContentView = new RemovalStateContentView1Acc();
            }
            m_ContentView.Initalize(this);

            m_BottomHint = new RectangleImage()
            {
                OutLineRectangle =
                    new Rectangle(274, m_ContentView.StateGrid.Max(m=> m.OutLineRectangle.Bottom) + 10, ImageResource.removalstate2.Width, ImageResource.removalstate2.Height),
                Image = ImageResource.removalstate2,
            };

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                switch ((ViewConfig)nParaB)
                {
                    case ViewConfig.RemovalState:
                        InitDate();
                        break;
                }
            }
        }

        public override string GetInfo()
        {
            return "�г�״̬";
        }

        public void InitDate()
        {
            m_TrainView.Location = GlobalInfo.CurrentCRH2Config.CutInfoConfig.TrainViewLocation.Rectangle.Location;
            m_TrainView.NeedDrawCarName = true;
            m_TrainView.IsUnitNameVisible = true;
        }

        public void OnDraw(Graphics g)
        {
            m_BottomHint.OnDraw(g);

            m_TrainView.paint(g);

            m_ContentView.OnDraw(g);

        }
    }
}