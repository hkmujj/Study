using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动测试.TestItems
{
    public class TestOfMrpContinuity : BrakeTestControl
    {
        public TestOfMrpContinuity(BrakeTestType brakeTestType, ControlBrakeTest srcObj) : base(brakeTestType, srcObj)
        {
            BrakeTestResult = new BrakeTestResult("MRP连续试验");
        }
        public override void ResetStates()
        {
            base.ResetStates();

            m_Content0 = "MRP连续试验:\n\n";
            m_Content1 = "-按\"开始试验\"键启动MRP连续试验";
        }
        public override string TitleName
        {
            get { return "制动试验; MRP连续试验"; }
        }
        protected override void ResetWhenStartTest()
        {
            base.ResetWhenStartTest();

            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubMRP贯通性试验开始), 1, 0);
        }

        protected override void UpdateContents()
        {
            switch (m_Step)
            {
                case 0:
                    m_Step = GetInFloatValue(InFloatKeys.Inf总风管_总) > 900 ? 1 : 2;
                    m_TheTimeCounter.ResetCounter();
                    break;
                case 1:
                    m_Content1 = "-将总风压力降至900kPa以下（进行若干次最大常用制动）";
                    if (GetInFloatValue(InFloatKeys.Inf总风管_总) < 880)
                    {
                        m_Step = 2;
                    }
                    break;
                case 2:
                    m_Content1 = "-MRP贯通性试验运行中\n等待1分钟后空压机使MRP压力升高最少30kPa\n\n-检查所有头车MRP压力是否升高是否30kPa";
                    if (m_TheTimeCounter.TimeUp(CurrentTime, 58))
                    {
                        m_Step = GetInFloatValue(InFloatKeys.Inf总风管_总) > 900 ? 3 : 0;
                        m_TheTimeCounter.ResetCounter();
                    }
                    if (m_Step == 3)
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubMRP贯通性试验结束), 1, 0);
                        //MRP连续试验结束
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub测试结果5106), 1, 0);
                        BrakeTestTime[2] = TestOverTime(); //结束MRP连续试验时间
                        BrakeTestResult.TestOver(CurrentTime);
                        BrakeTestResult.TestOver(CurrentTime);
                    }
                    break;
                case 3:
                    m_Content1 = "-MRP贯通性试验运行中\n-检查所有头车MRP压力是否升高是否30kPa\n\n\nMRP贯通性试验结束\n-按\"制动试验\"键继续";
                    break;
            }
        }
    }
}