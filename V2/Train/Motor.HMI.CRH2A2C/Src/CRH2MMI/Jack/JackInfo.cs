using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using CRH2MMI.Common;
using CRH2MMI.Common.Global;
using MMI.Facility.Interface.Attribute;

namespace CRH2MMI.Jack
{
    /// <summary>
    /// 配电盘信息
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    class JackInfo : CRH2BaseClass
    {

        private List<JackGroup> m_JackGroups;

        private JackGroup m_SelectJackGroup;

        public override void paint(Graphics g)
        {
            m_SelectJackGroup.OnDraw(g);
        }

        protected override bool OnMouseDown(Point nPoint)
        {
            return m_SelectJackGroup.OnMouseDown(nPoint);

        }

        public override bool Init()
        {

            InitUIObj(GlobalInfo.CurrentCRH2Config.JackConfig.JackOfCars.SelectMany(s => s.Units));

            InitGroups();

            return true;
        }

        private void InitGroups()
        {
            m_JackGroups = JackGroup.CreateGroups(this, GlobalInfo.CurrentCRH2Config.JackConfig);
            m_JackGroups.ForEach(e => e.ChangeGroup += group => ReselectGroup(m_JackGroups.Find(f => f != group)));
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            base.setRunValue(nParaA, nParaB, nParaC);
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                ReselectGroup(m_JackGroups.First());
            }
        }

        private void ReselectGroup(JackGroup jackGroup)
        {
            m_SelectJackGroup = jackGroup;
            m_SelectJackGroup.ReselectCarNo();
        }

        public override string GetInfo()
        {
            return "配电盘信息";
        }
    }
}