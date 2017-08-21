using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动测试.TestItems
{
    public class TestOfDirectBrake : BrakeTestControl
    {


        public TestOfDirectBrake(BrakeTestType brakeTestType, ControlBrakeTest srcObj) : base(brakeTestType, srcObj)
        {
            BrakeTestResult=new BrakeTestResult("直接制动试验");
        }
        public override void ResetStates()
        {
            base.ResetStates();


            m_Content0 = "直接制动试验:\n\n";
            m_Content1 = "-按\"开始试验\"键启动直接制动试验";
        }
        public override string TitleName
        {
            get { return "制动试验; 直接制动试验"; }
        }

        protected override void ResetWhenStartTest()
        {
            base.ResetWhenStartTest();
            append_postCmd(CmdType.SetBoolValue, m_SrcObj.GetOutBoolIndex(OutBoolKeys.Oub直接制动试验开始), 1, 0);

        }

        protected override void UpdateContents()
        {
            switch (m_Step)
            {
                case 0:
                    m_Content1 = "-检查是否所有制动不起作用\n-将制动手柄置于“3”位";
                    if (GetInBoolValue(InBoolKeys.Inb制动测试1373))
                    {
                        m_Step = 1;
                    }
                    m_TheTimeCounter.ResetCounter();
                    break;
                case 1:
                    m_Content1 = "-检查是否所有制动已施加\n 制动保持不短于10秒";
                    if (!GetInBoolValue(InBoolKeys.Inb制动测试1373))
                    {
                        m_TheTimeCounter.ResetCounter();
                        m_Step = 0;
                    }
                    else if (m_TheTimeCounter.TimeUp(CurrentTime, 10))
                    {
                        m_Step = 2;
                        m_TheTimeCounter.ResetCounter();
                    }
                    break;
                case 2:
                    m_Content1 = "-检查是否所有制动已施加\n 制动保持不短于10秒\n\n -将制动手柄置于缓解位";
                    if (GetInBoolValue(InBoolKeys.Inb制动测试1371))
                    {
                        m_Step = 3;
                    }
                    break;
                case 3:
                    m_Content1 = "-检查是否所有制动已缓解\n 所有缓解不短于10秒";
                    if (!GetInBoolValue(InBoolKeys.Inb制动测试1371))
                    {
                        m_TheTimeCounter.ResetCounter();
                        m_Step = 2;
                    }
                    else if (m_TheTimeCounter.TimeUp(CurrentTime, 10))
                    {
                        m_Step = 4;
                        m_TheTimeCounter.ResetCounter();
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub直接制动试验结束), 1, 0); //结束直接制动试验
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub测试结果5104), 1, 0); //结束直接制动试验
                        BrakeTestTime[0] = TestOverTime(); //结束直接制动试验时间
                        BrakeTestResult.TestOver(CurrentTime);
                    }
                    break;
                case 4:
                    m_Content1 = "-检查是否所有制动已缓解\n 所有缓解不短于10秒\n\n\n\n-直接制动试验结束\n-按\"制动试验\"键继续";
                    break;
            }
        }
    }
}