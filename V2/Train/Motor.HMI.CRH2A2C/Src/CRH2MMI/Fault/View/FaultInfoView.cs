using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace CRH2MMI.Fault.View
{
    /// <summary>
    /// 故障信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class FaultInfoView : CRH2BaseClass
    {
        private TrainView m_TrainView;

        private List<FaultInfoPageBase> m_FaultInfoPageBases;

        public FaultInfoPageBase CurrentFautlPage { get; private set; }

        private PackingBrakeCutOccuseView m_BrakeCutOccuseView;

        private Title.Title m_Title;

        public override void paint(Graphics g)
        {
            CurrentFautlPage.OnDraw(g);

            m_BrakeCutOccuseView.OnPaint(g);
        }

        protected override bool OnMouseDown(Point point)
        {
            if (!m_Title.HasPackingBrakeCutEvent)
            {
                return CurrentFautlPage.OnMouseDown(point);
            }

            m_BrakeCutOccuseView.OnMouseDown(point);

            return true;
        }

        public void GetoFaultInfoPage()
        {
            CurrentFautlPage = m_FaultInfoPageBases.First();
            m_TrainView.Location = GlobalInfo.CurrentCRH2Config.FaultConfig.TrainViewLocation.Rectangle.Location;
            m_Title.TitleMenuClicking += CurrentFautlPage.OnTitleMenuClick;
        }

        public void GotoDetailFaultInfoPage()
        {
            var location = new Point(m_TrainView.Location.X, 140);
            m_TrainView.Location = location;
            CurrentFautlPage = m_FaultInfoPageBases[1];
            m_Title.TitleMenuClicking += CurrentFautlPage.OnTitleMenuClick;
        }

        public override bool Init()
        {
            m_TrainView = TrainView.Instance;

            m_Title = Title.Title.Instance;

            m_FaultInfoPageBases = new List<FaultInfoPageBase>()
                                   {
                                       new FaultInfoPage(m_Title, this, m_TrainView),
                                       new FaultDetailInfoPage(m_Title, this, m_TrainView),
                                   };
            CurrentFautlPage = m_FaultInfoPageBases.First();

            m_BrakeCutOccuseView = new PackingBrakeCutOccuseView();
            m_BrakeCutOccuseView.Init();
            m_BrakeCutOccuseView.RefreshAction = o =>
            {
                m_BrakeCutOccuseView.HasOccuse = m_Title.HasPackingBrakeCutEvent;
            };
            m_BrakeCutOccuseView.ConfirmEvent += () =>
            {
                m_Title.HasPackingBrakeCutEvent = false;
                append_postCmd(CmdType.ChangePage, (int) ViewConfig.DriveState, 0, 0);
            };

            PackingBrakeCutManager.Instance.PackingBrakeCutChangedEvent += () =>
            {
                if (PackingBrakeCutManager.Instance.HasAnyNotConfirm)
                {
                    CurrentFautlPage = m_FaultInfoPageBases.First();
                }
            };

            SetValueWhenDebug();

            return true;
        }


        private void SetValueWhenDebug()
        {
            append_postCmd(CmdType.SetInBoolValue, 1633, 1, 0);
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                InitDate();
                m_TrainView.ResetCarState();
                foreach (var car in m_TrainView.Cars)
                {
                    car.CarStateProxy = new CarStateProxy() { UserSetState = CarState.Normal };
                }
            }
        }

        public override string GetInfo()
        {
            return "故障信息";
        }


        public void InitDate()
        {
            CurrentFautlPage = m_FaultInfoPageBases.First();
            m_TrainView.Location = GlobalInfo.CurrentCRH2Config.FaultConfig.TrainViewLocation.Rectangle.Location;
            //m_TrainView.IsCarStateAutoChangable = false;
            m_Title.TitleMenuClicking += CurrentFautlPage.OnTitleMenuClick;
            m_Title.TitleShowStrategy = new FaultViewTitleTitleShowStrategy(this);
        }

    }
}