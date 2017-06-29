namespace CBTC.Infrasturcture.Model.Constant
{
    public enum BrakeType
    {
        /// <summary>
        /// 无制动施加
        ///        制动预警
        ///快速制动
        ///请求制动
        ///允许缓解
        ///紧急制动
        ///切除牵引
        ///
        /// </summary>
        None,
        /// <summary>
        /// 切除牵引
        /// </summary>
        CutTow,

        /// <summary>
        /// 常用制动
        /// </summary>
        NormalBrake,

        /// <summary>
        /// 紧急制动
        /// </summary>
        EmergBrake,

    }
}