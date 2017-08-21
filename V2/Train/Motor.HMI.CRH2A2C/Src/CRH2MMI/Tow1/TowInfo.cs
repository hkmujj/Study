using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Config;
using CRH2MMI.Common.Control;
using CRH2MMI.Common.Global;
using CRH2MMI.Common.View.Train;
using CRH2MMI.Config.ConfigModel;
using CRH2MMI.Tow1.Pages;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;

namespace CRH2MMI.Tow1
{
    /// <summary>
    /// 牵引变流器信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class TowInfo : CRH2BaseClass
    {
        private TrainView m_TrainView;

        private List<Tow1InfoPageBase> m_PageBases;

        // ReSharper disable once InconsistentNaming
        protected Tow1InfoPageBase m_SelectedPageBase;

        private List<CRH2Button> m_ControlBtns;

        public override void paint(Graphics g)
        {
            m_SelectedPageBase.OnPaint(g);
            m_ControlBtns.ForEach(e => e.OnDraw(g));
        }

        public override bool Init()
        {

            m_TrainView = TrainView.Instance;

            var tow1Config = GlobalInfo.CurrentCRH2Config.Tow1Config;

            InitUIObj(tow1Config.Tow1PageConfigs.SelectMany(s => s.Grid.Rows).SelectMany(s => s.ColumnConfigs).Cast<CRH2CommunicationPortModel>());

            tow1Config.Tow1PageConfigs.ForEach(e => e.Grid.RefreshRelation());
            tow1Config.Correction();

            m_PageBases = new List<Tow1InfoPageBase>()
            {
                new Tow1Page(this),
                new MonitorElectricityPage(this),
                new DCValtagePage(this),
                new MonitorRotorPage(this),
                new RegenerationBrakePage(this),
            };

            m_PageBases.ForEach(e => e.Init());

            InitCtolBtns();

            ReselectBtn(m_ControlBtns.First());

            return true;
        }

        private void InitCtolBtns()
        {
            m_ControlBtns = new List<CRH2Button>();
            var pageNames = m_PageBases.Select(s => s.PageName.Length >= 5 ? s.PageName.Insert(2, "\r\n") : s.PageName).ToList();
            for (int i = 0; i < pageNames.Count; i++)
            {
                m_ControlBtns.Add(new CRH2Button()
                {
                    OutLineRectangle = new Rectangle(170 + 100 * i, 525, 100, 45),
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
            var btn = (CRH2Button)sender;

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

                switch ((ViewConfig)nParaB)
                {
                    case ViewConfig.Tow1:
                        m_TrainView.Location =
                            GlobalInfo.CurrentCRH2Config.Tow1Config.TrainViewLocation.Rectangle.Location;
                        break;
                }
            }
        }

        public override string GetInfo()
        {
            return "牵引变流器 编";
        }
    }
}