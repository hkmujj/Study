using MsgReceiveMod;

namespace Motor.HMI.CRH3C380B.Base.底层共用
{
    public class FaultInfo : FaultMsgOrdinary
    {
        /// <summary>
        /// 车厢编号
        /// </summary>
        public string CarriageID { get; set; }

        /// <summary>
        /// 显示的车厢号全称：机车号+车厢号。
        /// </summary>
        public string FullCarriageID
        {
            get { return CarriageID; }
        }

        /// <summary>
        /// 故障类型
        /// </summary>
        public string FaultTypeName { get; set; }

        /// <summary>
        /// 故障名称
        /// </summary>
        public string FaultName { get; set; }


        /// <summary>
        /// v=0情况下故障排除方法
        /// </summary>
        public string VelocityIs0 { get; set; }

        /// <summary>
        /// v>0情况下故障排除方法
        /// </summary>
        public string VelocityIsnot0 { get; set; }
    }
}