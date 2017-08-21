using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动测试.TestItems
{
    public class TestOfBpLeakage : BrakeTestControl
    {
        private float m_BpValue;

        public TestOfBpLeakage(BrakeTestType brakeTestType, ControlBrakeTest srcObj) : base(brakeTestType, srcObj)
        {
            BrakeTestResult = new BrakeTestResult("IBP泄漏试验");
        }
        public override void ResetStates()
        {
            base.ResetStates();


            m_Content0 = "BP泄漏试验:\n\n";
            m_Content1 = "-按\"开始试验\"键启动BP泄漏试验";
        }
        public override string TitleName
        {
            get { return "制动试验; BP泄漏试验"; }
        }
        protected override void ResetWhenStartTest()
        {
            base.ResetWhenStartTest();

            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubBP泄漏试验开始 ), 1, 0);
            m_BpValue = GetInFloatValue(InFloatKeys.Inf列车管_总);
        }

        protected override void UpdateContents()
        {
            switch (m_Step)
            {
                case 0:
                    m_Content1 = "-BP充风已切断\n\n检查BP管压力在30秒内不降低";
                    if (m_BpValue >= GetInFloatValue(InFloatKeys.Inf列车管_总))
                    {
                        if (m_TheTimeCounter.TimeUp(CurrentTime, 30))
                        {
                            m_Step = 1;
                            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubBP泄漏试验结束), 1, 0);
                            //BP泄漏试验结束
                            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub测试结果5107), 1, 0);
                            BrakeTestTime[3] = TestOverTime(); //结束BP泄漏试验时间
                            BrakeTestResult.TestOver(CurrentTime);
                        }
                    }
                    else
                    {
                        m_TheTimeCounter.ResetCounter();
                    }

                    m_BpValue = GetInFloatValue(InFloatKeys.Inf列车管_总);
                    break;
                case 1:
                    m_Content1 = "-BP充风已切断\n检查BP管压力在30秒内不降低\n\n\nBP泄漏试验完成\n-按\"制动试验\"键继续";
                    break;
            }
        }
    }
}