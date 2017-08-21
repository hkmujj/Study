using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动测试.TestItems
{
    public class TestOfEmergenceUB : BrakeTestControl
    {
        public TestOfEmergenceUB(BrakeTestType brakeTestType, ControlBrakeTest srcObj) : base(brakeTestType, srcObj)
        {
            BrakeTestResult = new BrakeTestResult("紧急制动UB试验");
        }

        public override string TitleName
        {
            get { return "制动试验; 紧急制动UB试验"; }
        }

        public override void ResetStates()
        {
            base.ResetStates();

            m_Content0 = "紧急制动UB试验:\n\n";
            m_Content1 = "-按\"开始试验\"键启动紧急制动试验";
        }

        protected override void ResetWhenStartTest()
        {
            //base.ResetWhenStartTest();

            //TODO base.ResetWhenStartTest(); 发送紧急制动UB试验
            //append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub紧急制动试验开始), 1, 0);
        }

        protected override void UpdateContents()
        {
            //TODO  替换所有接口和文字 。
            switch (m_Step)
            {
                case 0:
                    m_Content1 = "-将制动手柄置于紧急制动位";
                    if (!GetInBoolValue(InBoolKeys.Inb制动测试1369)){ m_Step = 1;}
                    m_TheTimeCounter.ResetCounter();
                    break;
                case 1:
                    m_Content1 = "-检查BP管压力是否降至200KPa以下\n-检查是否所有制动已施加";
                    if (GetInFloatValue(InFloatKeys.Inf列车管_总) <= 200 && GetInBoolValue(InBoolKeys.Inb制动测试1373))
                    {
                        m_Step = 2;
                    }
                    break;
                case 2:
                    m_Content1 = "-检查BP管压力是否降至200KPa以下\n-检查是否所有制动已施加\n-将制动手柄置于过充风位";
                    if (GetInFloatValue(InFloatKeys.Inf列车管_总) <= 200 && GetInBoolValue(InBoolKeys.Inb制动测试1373) &&
                        GetInBoolValue(InBoolKeys.Inb制动测试1369))
                    {
                        m_Step = 3;
                    }
                    break;
                case 3:
                    m_Content1 = "-检查BP管压力是否上升至600KPa以上\n-检查是否所有制动已缓解";
                    if (GetInFloatValue(InFloatKeys.Inf列车管_总) >= 600 && GetInBoolValue(InBoolKeys.Inb制动测试1371))
                    {
                        m_Step = 4;
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub紧急制动试验结束), 1, 0); //紧急制动试验结束
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub测试结果5105), 1, 0);
                        BrakeTestTime[1] = TestOverTime(); //结束紧急制动试验时间
                        BrakeTestResult.TestOver(CurrentTime);
                    }
                    break;
                case 4:
                    m_Content1 = "-检查BP管压力是否上升至600KPa以上\n-检查是否所有制动已缓解\n\n\n\n紧急制动试验结束\n-按\"制动试验\"键继续";
                    break;
            }
        }
    }
}