using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.系统.Coupling;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMICoupling : CRH3C380BBase
    {
        private List<CouplingContentView> m_ContentViewCollection;

        private CouplingContentView m_CurrentContentView;

        //2
        public override string GetInfo()
        {
            return "DMI 编组";
        }

        //6
        public override bool Initalize()
        {
            m_ContentViewCollection = new List<CouplingContentView>
            {
                new CouplingCouplingContentView(this)
            };

            m_CurrentContentView = m_ContentViewCollection[0];

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2 || DMITitle.BtnContentList.Count == 0)
            {
                return;
            }

            m_CurrentContentView.Reset();
        }

        public override void Paint(Graphics g)
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //C键
                        append_postCmd(CmdType.ChangePage, 120, 0, 0);
                        break;
                }
            }

            m_CurrentContentView.PrepareReady(BoolList[GetInBoolIndex("自动切换到ASC速度设定为2km/h的连挂界面")]);
            m_CurrentContentView.RefreshVisible(BoolList[GetInBoolIndex("联挂成功")]);
            m_CurrentContentView.RefreshTitleName();
            m_CurrentContentView.OnPaint(g);
            m_CurrentContentView.DrawSpeed(g);
        }
    }
}