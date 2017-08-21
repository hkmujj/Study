using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using CRH2MMI.Config.ConfigModel;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.PowerVoltage
{
    /// <summary>
    /// 电源电压
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class Power : CRH2BaseClass
    {
        private List<PowerVoltagePageBase> m_PageBases;

        private PowerVoltagePageBase m_SelectedPage;


        public override void paint(Graphics g)
        {
            m_SelectedPage.OnPaint(g);
        }

        public override bool Init()
        {

            GlobalInfo.CurrentCRH2Config.PowerVoltageConfig.Correction();

            var modes =
                GlobalInfo.CurrentCRH2Config.PowerVoltageConfig.FirstPageModels.SelectMany(
                    s => s.VoltageConfigs).ToList();
            modes.AddRange(GlobalInfo.CurrentCRH2Config.PowerVoltageConfig.SecondPageModels
                .SelectMany(s => s.VoltageConfigs));

            InitUIObj(modes.Cast<CRH2CommunicationPortModel>());

            m_PageBases = new List<PowerVoltagePageBase>()
            {
                new PowerVoltagePageOne(this) {ChangePage = OnChangePage},
                new PowerVoltagePageTwo(this) {ChangePage = OnChangePage},
            };

            m_SelectedPage = m_PageBases.First();

            return true;
        }

        private void OnChangePage(object sender, EventArgs eventArgs)
        {
            m_SelectedPage = m_PageBases.Find(f => f != m_SelectedPage);
        }

        protected override bool OnMouseDown(Point nPoint)
        {
            return m_SelectedPage.OnMouseDown(nPoint);
        }


        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == 2)
            {
                m_SelectedPage = m_PageBases.First();
            }
        }

        public override string GetInfo()
        {
            return "电源电压";
        }
    }
}