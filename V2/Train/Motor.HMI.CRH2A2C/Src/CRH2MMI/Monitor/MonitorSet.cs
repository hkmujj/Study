using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CommonUtil.Util;
using CRH2MMI.Monitor.Pages;
using MMI.Facility.Interface.Attribute;
using CommonUtil.Controls;
using CRH2MMI.Common;


namespace CRH2MMI.Monitor
{
    /// <summary>
    /// 监控设定
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class MonitorSet : CRH2BaseClass
    {
        public EventHandler MonitorDateTimeChanged;

        private List<CommonInnerControlBase> m_Pages;
        private CommonInnerControlBase m_SelectedPage;

        /// <summary>
        /// 监控时间
        /// </summary>
        public DateTime MonitorDateTime
        {
            set
            {
                m_MonitorDateTime = value;
                HandleUtil.OnHandle(MonitorDateTimeChanged);
            }
            get { return m_MonitorDateTime; }
        }

        /// <summary>
        /// 轮径
        /// </summary>
        public List<float> WheelDiameters { private set; get; }

        private DateTime m_MonitorDateTime;

        public MonitorSet()
        {
            WheelDiameters = new List<float>() { 830, 830 };
        }

        public override void paint(Graphics g)
        {
            m_SelectedPage.OnPaint(g);
        }

        public override bool Init()
        {

            m_Pages = new List<CommonInnerControlBase>()
            {
                new SettingControlPage(this)
                {
                    ChangePage = (sender, args) =>
                    {
                        switch (args.To)
                        {
                            case ChangedToPageEventArgs.ChangeTo.Date:
                                m_SelectedPage = m_Pages[1];
                                ((MonitorSetPageBase)m_SelectedPage).Reset();
                                break;
                            case ChangedToPageEventArgs.ChangeTo.Time:
                                m_SelectedPage = m_Pages[2];
                                ((MonitorSetPageBase)m_SelectedPage).Reset();
                                break;
                            default:
                                throw new ArgumentOutOfRangeException();
                        }
                    }
                },
                new MonitorSetDatePage(this) {ChangePageBack = OnChangePageBack},
                new MonitorSetTimePage(this) {ChangePageBack = OnChangePageBack},
            };
            m_Pages.ForEach(e => e.Init());

            MonitorDateTime = CurrentTime;

            m_SelectedPage = m_Pages[0];

            return true;
        }

        private void OnChangePageBack(object sender, EventArgs eventArgs)
        {
            m_SelectedPage = m_Pages[0];
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                m_SelectedPage = m_Pages.First();
            }
        }

        public override string GetInfo()
        {
            return "监控设定";
        }

        protected override bool OnMouseDown(Point point)
        {
            return m_SelectedPage.OnMouseDown(point);

        }
    }
}