using System.Drawing;
using MMI.Facility.Interface.Attribute;
using Motor.HMI.CRH3C380B.Common;

namespace Motor.HMI.CRH3C380B.Base.基本画面
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class DMIBasePage : CRH3C380BBase
    {

        private BasePageContent m_ContentControl;

        public override string GetInfo()
        {
            return "DMI基本页";
        }

        //6
        public override bool Initalize()
        {
            InitData();
            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 1)
            {

            }
            if (nParaA != 2 || DMITitle.BtnContentList.Count == 0)
            {
                return;
            }

            for (var i = 0; i < 10; i++)
            {
                DMITitle.BtnContentList[i].BtnStr = m_ContentControl.BtnStr[i];
            }
        }

        public override void Paint(Graphics g)
        {
            m_ContentControl.OnPaint(g);
        }


        private void InitData()
        {
            switch (GlobalParam.Instance.ProjectType)
            {
                case ProjectType.CRH3C:
                    m_ContentControl = new BasePage3C(this);
                    break;
                case ProjectType.CRH380B:
                    m_ContentControl = new BasePage380B(this);
                    break;
                case ProjectType.CRH380BL:
                    m_ContentControl = new BasePage380BL(this);
                    break;
            }

            m_ContentControl.Init();
        }
    }
}