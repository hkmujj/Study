using System;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.CarInfo
{
    /// <summary>
    /// 车辆信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class CarInfo : CRH2BaseClass
    {
        /// <summary>
        /// 当前选中页
        /// </summary>
        private CarInfoPageBase m_SelectedPage;

        /// <summary>
        /// 第一页
        /// </summary>
        private CarInfoPage1 m_CarInfoPage1;

        /// <summary>
        /// 第二页
        /// </summary>
        private CarInfoPage2 m_CarInfoPage2;

        private CarInfoResource m_CarInfoResource;
        private TrainView m_TrainView;

        public CarInfoResource Resource
        {
            get { return m_CarInfoResource; }
        }

       public override void paint(Graphics g)
        {
            OnDraw(g);
        }

        public override bool Init()
        {
            m_TrainView = TrainView.Instance;

            InitDate();

            m_CarInfoResource = new CarInfoResource(this);

            InitUIObj();

            m_CarInfoPage1 = new CarInfoPage1(this);
            m_CarInfoPage1.Init();
            m_CarInfoPage1.ChangePageEvent = OnChangePage;

            m_CarInfoPage2 = new CarInfoPage2(this);
            m_CarInfoPage2.Init();
            m_CarInfoPage2.ChangePageEvent = OnChangePage;

            return true;
        }

        private void InitUIObj()
        {
            var models =
                m_CarInfoResource.CarInfoConfig.Page1UpGrid.SelectMany(s => s.Rows).SelectMany(s => s.ColumnConfigs)
                    .Cast<CRH2CommunicationPortModel>().ToList();
            models.AddRange(m_CarInfoResource.CarInfoConfig.Page1DownGrid.SelectMany(s => s.Rows).SelectMany(s => s.ColumnConfigs));
            models.AddRange( m_CarInfoResource.CarInfoConfig.Page2Grid.SelectMany(s => s.Rows).SelectMany(s => s.ColumnConfigs));

            InitUIObj(models);

        }

        private void OnChangePage(object sender, EventArgs e)
        {
            var arg = e as ChangeCarInfoPageEventArgs;
            Debug.Assert(arg != null, "arg != null");
            switch (arg.Type)
            {
                case ChangePageType.ToPage1:
                    m_SelectedPage = m_CarInfoPage1;
                    break;
                case ChangePageType.ToPage2:
                    m_SelectedPage = m_CarInfoPage2;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected override bool OnMouseDown(Point nPoint)
        {
            return m_SelectedPage.OnMouseDown(nPoint);
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_SelectedPage = m_CarInfoPage1;
                switch (nParaB)
                {
                    case 3:
                        InitDate();
                        break;
                }
            }
        }

        public override string GetInfo()
        {
            return "车辆信息";
        }

        public void InitDate()
        {
            m_TrainView.Location = GlobalInfo.CurrentCRH2Config.CarInfoConfig.TrainViewLocation.Rectangle.Location;
        }

        public void OnDraw(Graphics g)
        {
            m_SelectedPage.OnDraw(g);
        }

    }
}