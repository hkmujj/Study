using System;
using System.Drawing;
using MMI.Facility.Interface.Attribute;
using MMI.Facility.Interface.Data;
using Motor.HMI.CRH1A.Common;
using Motor.HMI.CRH1A.Common.Global;
using Motor.HMI.CRH1A.Resource;

namespace Motor.HMI.CRH1A
{
    [GksDataType(DataType.isMMIObjectClass)]
    public class ExternalButtons : CRH1BaseClass
    {
        private bool m_BrakeTest;

        public bool BrakeTest
        {
            set
            {
                if (!Equals(value, m_BrakeTest))
                {
                    m_BrakeTest = value;
                    if (m_BrakeTest &&
                        Math.Abs(
                            Math.Abs(GlobalInfo.Instance.Crh1AConfig.AdaptConfig.CurrentSpeedCoefficient* FloatList[IndexDescription.InFloatDescriptionDictionary[InFloatKeys.InF列车速度]])) <
                        float.Epsilon)
                    {
                        // 制动试验
                        append_postCmd(CmdType.ChangePage, 30, 0, 0);
                    }
                }
            }
        }

        public override string GetInfo()
        {
            return "外部按键";
        }

        public override bool Initialize()
        {
            return true;
        }

        public override void paint(Graphics g)
        {
            BrakeTest = BoolList[IndexDescription.InBoolDescriptionDictionary[InBoolKeys.InB外部制动试验按键]];
        }
    }
}