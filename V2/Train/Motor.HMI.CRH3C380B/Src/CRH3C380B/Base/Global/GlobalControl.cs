using System.Collections.Generic;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.define;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Common;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.Global
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class GlobalControl : CRH3C380BBase
    {
        private List<IViewControl> m_ViewControlCollection;

        public override bool Initalize()
        {
            m_ViewControlCollection = new List<IViewControl>
            {
                new RestartControl(),
            };

            m_ViewControlCollection.ForEach(e => e.Initalize(this));

            return true;
        }

        public override void setRunValue(int nParaA, int nParaB, float nParaC)
        {
            if (nParaA == SetRunValueDefine.ParaADefine.SwitchFromOther)
            {
                //复位完成
                if (nParaB == 3)
                {
                    m_ViewControlCollection.ForEach(e => e.Reset());
                }

                UpdateCurrentView(nParaB);
            }

            UpdateBrakeTestStates();
        }

        private void UpdateCurrentView(int nParaB)
        {
            var index = OutFloatKeys.Ouf给评价1屏处于的界面编号;
            if (IsRightScreen)
            {
                index = OutFloatKeys.Ouf给评价2屏处于的界面编号;
            }

            append_postCmd(CmdType.SetFloatValue, IndexDescriptionConfig.OutFloatDescriptionDictionary[index], 0,
                nParaB);
        }

        private void UpdateBrakeTestStates()
        {
            if (GetInBoolAt(InBoolKeys.Inb逻辑收到间接制动试验成功标志后反馈的标志))
            {
                SetOutBool(OutBoolKeys.Oub制动试验间接试验成功, 0);
            }
        }

        public override void Paint(Graphics g)
        {
            m_ViewControlCollection.ForEach(e => e.Refresh(this, g));
        }
    }
}