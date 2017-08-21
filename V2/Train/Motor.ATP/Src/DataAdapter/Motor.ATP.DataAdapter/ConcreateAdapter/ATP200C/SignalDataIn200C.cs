using System;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200C
{
    [Serializable]
    public class SignalDataIn200C : SignalDataIn
    {

        /// <summary>
        /// LKJ车次号头
        /// </summary>
        public float LKJTrainNumHead { set; get; }

        /// <summary>
        /// LKJ车次号
        /// </summary>
        public float LKJTrainNum { set; get; }

        /// <summary>
        /// LKJ司机号
        /// </summary>
        public float LKJDriverNum { set; get; }


        /// <summary>
        /// 请求请缓解列车制动
        /// </summary>
        public bool TrainBrakeReleaseAck { set; get; }
        /// <summary>
        /// 紧急制动状态
        /// </summary>
        public int nBreakTestStatus_Emerg { set; get; }
        /// <summary>
        /// 最大常用制动状态
        /// </summary>
        public int nBreakTestStatus_Break7 { set; get; }
        /// <summary>
        /// 目视警惕确认
        /// </summary>
        public bool OSAlarmAck { set; get; }
        /// <summary>
        /// 引导警惕确认
        /// </summary>
        public bool SRAlarmAck { set; get; }
        /// <summary>
        /// 目视使能
        /// </summary>
        public bool  OSButtonStatus { set; get; }


        /// <summary>
        /// 黑屏
        /// </summary>
        public bool ScreenBlackFlag { set; get; }

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

        
    
        //内部使用
        public bool BreakTestFlag { set; get; }
    

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}
