using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.系统.Decoupling;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.系统
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIDecoupling : CRH3C380BBase
    {

        private readonly string[] m_BtnStr =
        {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "主页面"
        };

        private DecouplingContentView m_CurrentDecouplingContentView;

        private DecouplingState m_CurrentDecouplingState;

        private Dictionary<DecouplingState, DecouplingContentView> m_DecouplingContentViewCollection;

        /// <summary>
        /// 当前解编状态
        /// </summary>
        public DecouplingState CurrentDecouplingState
        {
            set
            {
                m_CurrentDecouplingState = value;
                m_CurrentDecouplingContentView = m_DecouplingContentViewCollection[value];
                m_CurrentDecouplingContentView.Reset();
            }
            get { return m_CurrentDecouplingState; }
        }


        //2
        public override string GetInfo()
        {
            return "DMI解编";
        }

        //6
        public override bool Initalize()
        {
            m_DecouplingContentViewCollection = new Dictionary<DecouplingState, DecouplingContentView>
            {
                {DecouplingState.Prepareing, new PrepareingDecouplingContentView(this)},
                {DecouplingState.Prepared, new PreparedDecouplingContentView(this)},
                {DecouplingState.ConfirmDecoupling, new ConfirmDecouplingContentView(this)},
                {DecouplingState.Decoupling, new DecouplingDecouplingContentView(this)},
                {DecouplingState.ConfirmTrainGroup, new ConfirmTrainGroupDecouplingContentView(this)}
            };

            CurrentDecouplingState = DecouplingState.Prepareing;

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA != 2 || DMITitle.BtnContentList.Count == 0)
            {
                return;
            }

            for (int i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_BtnStr[i];
            }

            DMITitle.TitleName = "解编";

            CurrentDecouplingState = DecouplingState.Prepareing;
            m_CurrentDecouplingContentView.Reset();

        }

        public override void Paint(Graphics g)
        {
            ResponseUser();

            m_CurrentDecouplingContentView.OnPaint(g);
        }

        private void ResponseUser()
        {
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 0: //C键
                        append_postCmd(CmdType.ChangePage, 120, 0, 0);
                        break;
                    case 15:
                        append_postCmd(CmdType.ChangePage, 11, 0, 0);
                        break;
                }
            }
        }
    }
}