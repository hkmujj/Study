using System;
using Motor.ATP.DataAdapter.Model;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP200H
{
    [Serializable]
    public class SignalDataIn200H : SignalDataIn
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
        /// 辅屏_上行指示灯状态
        /// </summary>
        public int AuxiliaryScreen_FrequencyUp { set; get; }
        /// <summary>
        /// 辅屏_下行指示灯状态
        /// </summary>
        public int AuxiliaryScreen_FrequencyDown { set; get; }
        /// <summary>
        /// 辅屏_分相有效指示灯状态
        /// </summary>
        public int AuxiliaryScreen_DFXValid { set; get; }
        /// <summary>
        /// 辅屏_分相执行指示灯状态
        /// </summary>
        public int AuxiliaryScreen_DFXExecute { set; get; }
        /// <summary>
        /// 辅屏_限制速度
        /// </summary>
        public float AuxiliaryScreen_dATPSpeedLimit { set; get; }
        /// <summary>
        /// 辅屏_当前速度
        /// </summary>
        public float AuxiliaryScreen_TrainSpeed{ set; get; }
        /// <summary>
        /// 辅屏_目标速度
        /// </summary>
        public float AuxiliaryScreen_GoalSpeed { set; get; }
        /// <summary>
        /// 辅屏_目标距离
        /// </summary>
        public float AuxiliaryScreen_GoalDistance { set; get; }



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
        /// 车载设备自动换系
        /// </summary>
        public bool ChangeHostAuto { set; get; }

       

        /// <summary>
        /// 应答器丢失确认
        /// </summary>
        public bool EnterBaliseLostAck { set; get; }

        /// <summary>
        /// 辅屏控制
        /// </summary>
        public bool AuxiliaryScreenControl { set; get; }
    
        //内部使用
        public bool BreakTestFlag { set; get; }
    

        public override void ClearInfo()
        {
            base.ClearInfo();
        }
    }
}
