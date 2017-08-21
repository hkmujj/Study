using MsgReceiveMod;

namespace Motor.HMI.CRH5A.底层共用.消息
{
    public class FaultMsg5 : FaultMsgOrdinary
    {
        /// <summary>
        /// 子系统
        /// </summary>
        public string SubSysName { get; set; }

        /// <summary>
        /// 车辆号
        /// </summary>
        public int TrainID { get; set; }


        /// <summary>
        /// 特殊显示方式
        /// </summary>
        public MsgShowType MsgShowType { get; set; }
    }
}