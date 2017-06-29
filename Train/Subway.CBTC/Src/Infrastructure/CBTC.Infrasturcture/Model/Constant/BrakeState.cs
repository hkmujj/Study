namespace CBTC.Infrasturcture.Model.Constant
{
    public enum BrakeState
    {   
        Null,
        /// <summary>
        /// 制动力不足
        /// </summary>
        Insufficient,

        /// <summary>
        /// 空转
        /// </summary>
        WheelSlip,

        /// <summary>
        /// 车辆空转打滑信息(正常范围)
        /// </summary>
        NormalRacing,

        /// <summary>
        /// 车辆空转打滑信息(严重)
        /// </summary>
        BadRacing,

        EmergeBrake
        
    }
}