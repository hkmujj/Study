using System;
using System.Drawing;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;

namespace Urban.NanJing.AirportLine.ATC.Casco
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class V0C0BlackView : baseClass
    {
        public Boolean IsBlackView
        {
            set
            {
                if (m_IsBlackView == value)
                    return;

                m_IsBlackView = value;
                if (m_IsBlackView)
                {
                    append_postCmd(CmdType.ChangePage, 104, 0, 0);
                }
                else
                {
                    append_postCmd(CmdType.ChangePage, 103, 0, 0);
                }
            }
        }
        private Boolean m_IsBlackView = false;

        public override string GetInfo()
        {
            return "黑屏";
        }


        public override bool init(ref int nErrorObjectIndex)
        {
            append_postCmd(CmdType.SetInBoolValue, UIObj.InBoolList[0], 1, 0);

            return true;
        }

        /// <summary>
        /// 获取到当前运行视图：根据当前视图设置按钮状态
        /// </summary>
        /// <param name="nParaA"></param>
        /// <param name="nParaB"></param>
        /// <param name="nParaC"></param>
        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == 2)
            {
                if (nParaB == 103) m_IsBlackView = false;
            }
        }

        public override void paint(Graphics g)
        {
            IsBlackView = BoolList[UIObj.InBoolList[0]];

        }
    }
}
