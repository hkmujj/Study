using System;
using Motor.ATP.DataAdapter.Model;
using Motor.ATP.DataAdapter.Util;

namespace Motor.ATP.DataAdapter.ConcreateAdapter.ATP300T
{
    [Serializable]
    public class SignalDataOut300T : SignalDataOut
    {

 //float__________________________________
        /// <summary>
        /// 系统自检计时
        /// </summary>
        public float TimeSpan { set; get; }
        
        
        
//bool__________________________________
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
        /// 取消制动测试
        /// </summary>
        public bool CancleBrakeTest { set; get; }

        /// <summary>
        /// C3选择临时变量
        /// </summary>
        public bool C3SelFlag { set; get; }

        public bool EnterC3SelFlag { set; get; }

        /// <summary>
        /// 屏跳转特殊快捷键
        /// </summary>
        public bool FastButton { set; get; }

//内部使用不输出
        /// <summary>
        //确认 CTCS-2级 确认取消
        /// </summary>
        public MessageConfirmType ConfirmOrCancelC2Sign { set; get; }
        /// <summary>
        //确认 CTCS-3级 确认取消
        /// </summary>
        public MessageConfirmType ConfirmOrCancelC3Sign { set; get; }

        /// <summary>
        //确认 CTCS-2级 确认取消
        /// </summary>
        public MessageConfirmType ConfirmOrCancelRBCInputSign { set; get; }
        /// <summary>
        //驾驶数据输入 确认取消
        /// </summary>
        public MessageConfirmType ConfirmOrCancelDriveDataInputSign { set; get; }
        /// <summary>
        //等级选择 选择C2还是C3 
        /// </summary>
        public MessageConfirmType ConfirmOrCancelLevelSign { set; get; }
        /// <summary>
        //列车数据输入 确认取消
        /// </summary>
        public MessageConfirmType ConfirmOrCancelTrainDataInputSign { set; get; }

        public override void ClearInfo()
        {
            base.ClearInfo();

            //C3SelFlag = false;

            //EnterC3SelFlag = false;

            TimeSpan = -1;
        }
    }
}