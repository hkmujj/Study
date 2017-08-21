using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.BrakeInfo.Pages;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;

namespace CRH2MMI.BrakeInfo
{
    /// <summary>
    /// 制动信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class BrakeInfo : CRH2BaseClass
    {

        private TrainView m_TrainView;

        private List<BrakeInfoPageBase> m_PageBases;

        // ReSharper disable once InconsistentNaming
        protected BrakeInfoPageBase m_SelectedPageBase;

        private List<CRH2Button> m_ControlBtns; 

        public override void paint(Graphics g)
        {
            m_SelectedPageBase.OnPaint(g);
            m_ControlBtns.ForEach(e => e.OnDraw(g));
        }

        public override bool Init()
        {
            m_TrainView = TrainView.Instance;

            var brakeConf = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig;

            InitUIObj(
                brakeConf.BrakePageConfigs.SelectMany(s => s.Grid.Rows)
                    .SelectMany(s => s.ColumnConfigs)
                    .Cast<CRH2CommunicationPortModel>());

            brakeConf.BrakePageConfigs.ForEach(e => e.Grid.RefreshRelation());
            brakeConf.Correction();

            m_PageBases = new List<BrakeInfoPageBase>()
            {
                new BrakePage(this),
                new BrakePressurePage(this),
                new ASPressurePage(this),
                new MRPressurePage(this),
                new EPValvePage(this),
                new RegenerationPage(this),
            };
            m_PageBases.ForEach(e => e.Init());

            InitCtolBtns();

            ReselectBtn(m_ControlBtns.First());

            return true;
        }

        private void InitCtolBtns()
        {
            m_ControlBtns = new List<CRH2Button>();
            var pageNames = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig.BrakePageConfigs.FindAll(
                    f => f.PageName != "Base").Select(s => s.PageName).ToList();
            for (int i = 0; i < pageNames.Count; i++)
            {
                m_ControlBtns.Add(new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(110 + 100 * i, 525, 100, 45),
                    DownImage = GlobalResource.Instance.BtnDownImage,
                    UpImage = GlobalResource.Instance.BtnUpImage,
                    ButtonDownEvent = OnCtrolButtonDownEvent,
                    Text = pageNames[i],
                    TextColor = Color.White,
                });
            }
        }

        private void OnCtrolButtonDownEvent(object sender, EventArgs eventArgs)
        {
            var btn = (CRH2Button) sender;

            ReselectBtn(btn);

        }

        private void ReselectBtn(CRH2Button btn)
        {
            if (m_SelectedPageBase != null)
            {
                m_ControlBtns[m_PageBases.IndexOf(m_SelectedPageBase)].CurrentMouseState = MouseState.MouseUp;
            }
            btn.CurrentMouseState = MouseState.MouseDown;
            m_SelectedPageBase = m_PageBases[m_ControlBtns.IndexOf(btn)];
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_ControlBtns.Any(a => a.OnMouseDown(point));
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ReselectBtn(m_ControlBtns.First());

                var currentView = (ViewConfig) nParaB;
                switch (currentView)
                {
                    case ViewConfig.BrakeInfo:
                        ModifyTrainLocation();
                        break;
                }

                var lastView = (ViewConfig) nParaC;
                if (lastView == ViewConfig.Marsh)
                {
                    ReselectBtn((m_ControlBtns[1]));
                }
            }
        }

        public override string GetInfo()
        {
            return "制动信息";
        }

        public void ModifyTrainLocation()
        {
            m_TrainView.Location = GlobalInfo.CurrentCRH2Config.BrakeInfoConfig.TrainViewLocation.Rectangle.Location;
        }
    }
}