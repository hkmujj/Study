using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动测试.TestItems
{
    public class TestOfIndirectBrake : BrakeTestControl
    {
        public TestOfIndirectBrake(BrakeTestType brakeTestType, ControlBrakeTest srcObj) : base(brakeTestType, srcObj)
        {
            BrakeTestResult = new BrakeTestResult("间接制动试验");
        }
        public override void ResetStates()
        {
            base.ResetStates();

            m_Content0 = "间接制动试验:\n\n";
            m_Content1 = "-按\"开始试验\"键启动间接制动试验";
        }
        public override string TitleName
        {
            get { return "制动试验; 间接制动试验"; }
        }
        protected override void ResetWhenStartTest()
        {
            base.ResetWhenStartTest();
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub间接制动试验开始), 1, 0);

            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub制动试验间接试验成功), 0, 0); //间接制动试验结束
            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub测试结果5108), 0, 0);

        }

        protected override void UpdateContents()
        {
            switch (m_Step)
            {
                case 0:
                    DMITitle.BtnContentList[2].BtnStr = "BP\n排风";
                    m_Content1 = "-检查所有制动未作用\n-检查是否所有制动已缓解\n 保持缓解至少10秒\n-按\"BP排风\"键,通过后部头车上的紧急制动阀\n使BP排风";
                    if (DMIButton.BtnUpList.Count != 0 && DMIButton.BtnUpList[0] == 8) //按排风按钮
                    {
                        DMITitle.BtnContentList[2].BtnStr = string.Empty;
                        m_Step = 1;
                    }
                    break;
                case 1:
                    m_Content1 = "-通过后部头车的紧急制动阀使BP排风\n-检查是否前部头车的BP压力低于260Kpa\n-检查是否所有制动已施加\n制动保持至少10秒";
                    if (GetInBoolValue(InBoolKeys.Inb制动测试1373) && GetInFloatValue(InFloatKeys.Inf列车管_总) < 260)
                    {
                        if (!m_TheTimeCounter.TimeUp(CurrentTime, 10))
                        {
                            return;
                        }
                        m_Step = 2;
                        m_TheTimeCounter.ResetCounter();
                    }
                    break;
                case 2:
                    DMITitle.BtnContentList[5].BtnStr = "停止\n排风";
                    m_Content1 =
                        "-通过后部头车的紧急制动阀使BP排风\n-检查是否前部头车的BP压力低于260Kpa\n-检查是否所有制动已施加\n制动保持至少10秒\n\n-按\"停止排风\"键停止BP排风";
                    if (DMIButton.BtnUpList.Count != 0 && DMIButton.BtnUpList[0] == 11) //按停止排风按钮
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub停止排风), 1, 0);
                        DMITitle.BtnContentList[5].BtnStr = string.Empty;
                        m_Step = 3;
                    }
                    break;
                case 3:
                    m_Content1 = "-检查BP管压力是否上升至600KPa以上\n-检查是否所有制动已缓解";
                    if (GetInFloatValue(InFloatKeys.Inf列车管_总) >= 600 && GetInBoolValue(InBoolKeys.Inb制动测试1371))
                    {
                        m_Step = 4;
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub制动试验间接试验成功), 1, 0);
                        //间接制动试验结束
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub测试结果5108), 1, 0);
                        BrakeTestTime[4] = TestOverTime(); //结束间接制动试验时间
                        BrakeTestResult.TestOver(CurrentTime);
                    }
                    break;
                case 4:
                    m_Content1 = "-检查BP管压力是否上升至600KPa以上\n-检查是否所有制动已缓解\n\n\n\n间接制动试验结束\n-按\"制动试验\"键继续";
                    break;
            }
        }
    }
}