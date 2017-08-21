using MMI.Facility.Interface.Data;
using Motor.HMI.CRH3C380B.Base.底层共用;
using Motor.HMI.CRH3C380B.Resource;

namespace Motor.HMI.CRH3C380B.Base.制动测试.TestItems
{
    public class TestOfBpContinuity : BrakeTestControl
    {
        public TestOfBpContinuity(BrakeTestType brakeTestType, ControlBrakeTest srcObj) : base(brakeTestType, srcObj)
        {
            BrakeTestResult = new BrakeTestResult("BP连续试验");
        }

        public override void ResetStates()
        {
            base.ResetStates();


            m_Content0 = "BP连续试验:\n\n";
            m_Content1 = "-按\"开始试验\"键启动BP连续试验";
        }

        public override string TitleName
        {
            get { return "制动试验; BP连续试验"; }
        }

        public override void ResponseBtnEvent()
        {
            base.ResponseBtnEvent();
            if (DMIButton.BtnUpList.Count != 0)
            {
                switch (DMIButton.BtnUpList[0])
                {
                    case 8: //BP排风
                        //if (!string.IsNullOrEmpty(DMITitle.BtnContentList[2].BtnStr))
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubBP排风), 1, 0);
                    }
                        break;
                }
            }
        }

        protected override void ResetWhenStartTest()
        {
            base.ResetWhenStartTest();

            append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubBP贯通性试验开始), 1, 0);
        }

        protected override void UpdateContents()
        {
            switch (m_Step)
            {
                case 0:
                    m_Content1 = "-通过后部头车的紧急制动阀使BP排风\n检查是否前部头车的BP压力低于260kPa";
                    if (GetInFloatValue(InFloatKeys.Inf列车管_总) < 260)
                    {
                        m_Step = 1;
                    }
                    break;
                case 1:
                    DMITitle.BtnContentList[5].BtnStr = "停止\n排风";
                    m_Content1 = "-通过后部头车的紧急制动阀使BP排风\n检查是否前部头车的BP压力低于260kPa\n\n-按\"停止排风\"键停止BP排风";
                    if (DMIButton.BtnUpList.Count != 0 && DMIButton.BtnUpList[0] == 11) //按停止排风按钮
                    {
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub直接制动试验结束1), 1, 0);
                        DMITitle.BtnContentList[5].BtnStr = string.Empty;
                        m_Step = 2;
                    }
                    break;
                case 2:
                    m_Content1 = "-通过后部头车的紧急制动阀使BP停止排风\n检查前部头车的BP压力是否升高至正常工作\n压力范围\n";
                    if (GetInFloatValue(InFloatKeys.Inf列车管_总) >= 600)
                    {
                        m_Step = 3;
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.OubBP贯通性试验结束), 1, 0); //BP连续试验
                        append_postCmd(CmdType.SetBoolValue, GetOutBoolIndex(OutBoolKeys.Oub测试结果5109), 1, 0);
                        BrakeTestTime[5] = TestOverTime(); //结束BP连续试验时间
                        BrakeTestResult.TestOver(CurrentTime);
                    }
                    break;
                case 3:
                    m_Content1 = "-检查尾部头车的BP压力是否升高至正常工作\n压力范围\n\nBP贯通性试验完成\n-按\"制动试验\"键继续";
                    break;
            }
        }
    }
    
}