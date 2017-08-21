using CBTC.DataAdapter.Model;
using System;

namespace CBTC.DataAdapter.ConcreateAdapter.TCT
{
    [Serializable]
    public class SignalDataInTCT : SignalDataIn
    {
        /// <summary>
        /// 反馈_制动测试试闸
        /// </summary>
        public bool FeedBackBrakeTestSwitch { set; get; }

        /// <summary>
        /// 反馈_制动测试缓解
        /// </summary>
        public bool FeedBackBrakeTestRelease { set; get; }

        /// <summary>
        /// 反馈_广播测试
        /// </summary>
        public bool FeedBackBroadcastTest { set; get; }

        /// <summary>
        /// 反馈_灯测试
        /// </summary>
        public bool FeedBackLampTest { set; get; }
        public SignalDataInTCT()
        {
            FeedBackBrakeTestSwitch = false;
            FeedBackBrakeTestRelease = false;
            FeedBackBroadcastTest = false;
            FeedBackLampTest = false;
        }

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}