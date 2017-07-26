using System;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200C
{
    [Serializable]
    public class SignalDataOut200C : SignalDataOut
    {

        /// <summary>
        /// 请求请缓解列车制动
        /// </summary>
        public bool TrainBrakeReleaseSign { set; get; }
        /// <summary>
        /// 目视警惕确认
        /// </summary>
        public bool OSAlarmSign { set; get; }
        /// <summary>
        /// 引导警惕确认
        /// </summary>
        public bool SRAlarmSign { set; get; }
        /// <summary>
        /// 紧急制动测试开始标志
        /// </summary>
        public bool BrakeTestEmerg { set; get; }
        /// <summary>
        /// 最大常用制动测试开始标志
        /// </summary>
        public bool BrakeTestBreak7 { set; get; }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }

    }
}
