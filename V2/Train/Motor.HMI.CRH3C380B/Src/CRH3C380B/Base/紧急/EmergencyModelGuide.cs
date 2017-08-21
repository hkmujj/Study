using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource.Images;

namespace Motor.HMI.CRH3C380B.Base.紧急
{
    /// <summary>
    /// 紧急模式指南
    /// </summary>
    [GksDataType(DataType.isMMIObjectClass)]
    internal class EmergencyModelGuide : CRH3C380BBase
    {
        private Rectangle m_ImageRectangle;

        private string[] m_BottonButtonContentCollection;

        public override bool Initalize()
        {
            m_ImageRectangle = new Rectangle(0, 44, 760, 440);

            m_BottonButtonContentCollection = new string[DMITitle.BtnContentList.Count];
            m_BottonButtonContentCollection[m_BottonButtonContentCollection.Length - 1] = "返回";

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == ParaADefine.SwitchFromOhter)
            {
                DMITitle.UpdateBtnContent(m_BottonButtonContentCollection);

                DMITitle.TitleName = "紧急模式指南";
            }
        }

        public override void Paint(Graphics g)
        {
            OnButtonEvent();

            g.DrawImage(CommonImages.EmergencyModelGuide, m_ImageRectangle);
        }

        private void OnButtonEvent()
        {
            if (DMIButton.BtnUpList.Count == 0)
            {
                return;
            }

            switch (DMIButton.BtnUpList[0])
            {
                case 0: //C键
                    append_postCmd(CmdType.ChangePage, 150, 0, 0);
                    break;
                case 15: //返回
                    append_postCmd(CmdType.ChangePage, 150, 0, 0);
                    break;
            }
        }
    }
}
