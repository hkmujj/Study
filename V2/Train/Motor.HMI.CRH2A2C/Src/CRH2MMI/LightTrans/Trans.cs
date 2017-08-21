using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.LightTrans
{
    /// <summary>
    /// 光传输状态
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Trans : CRH2BaseClass
    {

        public TrainView TrainView { get; private set; }

        private LightTransView m_LightTransView;

        public TransResource Resource { private set; get; }

        public override void paint(Graphics g)
        {
            m_LightTransView.OnDraw(g);

            TrainView.paint(g);
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_LightTransView.OnMouseDown(point);
        }

        public override bool Init()
        {
            InitUIObj(GlobalInfo.CurrentCRH2Config.LightTrainsConfig.LightTransPages.SelectMany(s => s.LightUnitConfigs).Cast<CRH2CommunicationPortModel>());

            Resource = new TransResource(this);

            TrainView = TrainView.Instance;

            m_LightTransView = new LightTransView(this);
            m_LightTransView.PageChanged += OnLightTransViewOnPageChanged;

            return true;
        }

        private void OnLightTransViewOnPageChanged(LightTransView lightTransView)
        {
            TrainView.Reset();
            if (lightTransView == null)
            {
                return;
            }
            MoveTrainLocation(lightTransView.CurrentPage.PageConfig.TrainViewLocation.Rectangle.Location);
            switch (lightTransView.CurrentPage.PageID)
            {
                case LightTransPageID.Page0:
                    break;
                case LightTransPageID.Page1:
                    foreach (var car in TrainView.Cars.Skip(10))
                    {
                        car.Visible = false;
                    }
                    break;
                default:
                    foreach (var car in TrainView.Cars.Take(6))
                    {
                        car.Visible = false;
                    }
                    break;
            }
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_LightTransView.ReselectPage();

            }
        }

        public void MoveTrainLocation(Point location)
        {
            TrainView.Location = location;

            const int head = 130;
            const int body = 50;

            int[] widths = GlobalInfo.CurrentCRH2Config.LightTrainsConfig.LightTransPages.Count == 1
                ? new[] { head, body, body, body, body, body, body, head }
                : new int[16] { head, body, body, body, body, body, body, body, body, body, body, body, body, body, body, head };
            TrainView.SetHeadCarWidth(widths.Select((s, i) => new CarWidthModel(i, s)).ToList());

        }

        public override string GetInfo()
        {
            return "光传输状态";
        }
    }
}