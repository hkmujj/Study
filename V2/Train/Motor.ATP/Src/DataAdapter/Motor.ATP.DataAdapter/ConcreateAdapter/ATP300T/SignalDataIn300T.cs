using System;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300T
{
    [Serializable]
    public class SignalDataIn300T : SignalDataIn
    {   

        /// <summary>
        /// 黑屏
        /// </summary>
     //   public bool ScreenBlackFlag { set; get; }

        /// <summary>
        /// 常用制动故障确认
        /// </summary>
        public bool FaultNormalBreakAck { set; get; }

        /// <summary>
        /// 越行选择确认
        /// </summary>
        public bool ExceSelAck { set; get; }

        /// <summary>
        /// 退行防护确认
        /// </summary>
        public bool BackProtectAck { set; get; }

        /// <summary>
        /// 紧急制动缓解确认
        /// </summary>
        public bool EBrakeRelAck { set; get; }

        /// <summary>
        /// 常用制动缓解确认
        /// </summary>
        public bool CBrakeRelAck { set; get; }

        /// <summary>
        /// 车载设备自动换系
        /// </summary>
        public bool ChangeHostAuto { set; get; }

        /// <summary>
        /// 进入目视模式
        /// </summary>
        public bool EnterOSAck { set; get; }

        /// <summary>
        /// 启动完成确认
        /// </summary>
        public bool StartFinishAck { set; get; }

        /// <summary>
        /// 应答器丢失确认
        /// </summary>
        public bool EnterBaliseLostAck { set; get; }

 
        ///// <summary>
        ///// 系统自检状态
        ///// </summary>
        //public bool SelfCheckStatus { set; get; }

        /// <summary>
        /// 启动流程相关
        /// </summary>
        //public bool StartDriverNumConfirmed { set; get; }

        //public bool StartCtcsSelected { set; get; }
        //public bool StartTrainNumConfirmed { set; get; }
        public bool StartTrainDataSelected { set; get; }
        public bool StartFrequencySelected { set; get; }
        //public bool StartFrequencyConfirmed { set; get; }
        //public bool StartTrainDataConfirmed { set; get; }

        public SignalDataIn300T() :
            base()
        {

        }

        public override void ClearInfo()
        {
            base.ClearInfo();
            //StartDriverNumConfirmed = false;
            //StartCtcsSelected = false;
            //StartTrainNumConfirmed = false;
            StartTrainDataSelected = false;
            StartFrequencySelected = false;
            //StartFrequencyConfirmed = false;
            //StartTrainDataConfirmed = false;
        }
    }
}
