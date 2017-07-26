using System;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300S
{
    [Serializable]
    public class SignalDataOut300S : SignalDataOut
    {
        /// <summary>
        /// 启动确认
        /// </summary>
        public bool StartSign { set; get; }

        /// <summary>
        /// 常用制动故障确认
        /// </summary>
        public bool FaultNormalBreakSign { set; get; }

        /// <summary>
        /// 防溜确认
        /// </summary>
        public bool SlideConfirmSign { set; get; }

        /// <summary>
        /// 紧急制动缓解
        /// </summary>
        public bool EBRelSign { set; get; }

        /// <summary>
        /// 常用制动缓解
        /// </summary>
        public bool CBRelSign { set; get; }

        /// <summary>
        /// 屏幕启动过程
        /// </summary>
        public bool StartProcessFlag { set; get; }

        /// <summary>
        /// 应答器丢失确认
        /// </summary>
        public bool BaliseLostSign { set; get; }

        /// <summary>
        /// 退行防护确认
        /// </summary>
        public bool BackProtectSign { set; get; }

        /// <summary>
        /// 选择越行确认
        /// </summary>
        public bool ExceSelSign { set; get; }

        /// <summary>
        /// 选择越行确认
        /// </summary>
        public bool CancleExceSelSign { set; get; }

        /// <summary>
        /// 启动列车数据确认
        /// </summary>
        public bool StartTrainDataSign { set; get; }

        /// <summary>
        /// 请求输入车次号
        /// </summary>
        public bool StartTrainNumSign { set; get; }

        /// <summary>
        /// 请求输入列车长度
        /// </summary>
        public bool StartTrainLengthSign { set; get; }

        /// <summary>
        /// 启动载频确认
        /// </summary>
        public bool StartFreqSign { set; get; }

        /// <summary>
        /// C3选择临时变量
        /// </summary>
        public bool C3SelFlag { set; get; }

        public bool EnterC3SelFlag { set; get; }

        public override void ClearInfo()
        {
            base.ClearInfo();

            C3SelFlag = false;

            EnterC3SelFlag = false;
        }
    }
}