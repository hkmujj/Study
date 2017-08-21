namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 车载定位状态
    /// 0、无
    /// 1、车辆已精确定位
    /// 2、车辆正在定位过程中
    /// 3、车辆失去定位
    /// </summary>
    public enum VehicleLocationStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 车辆已精确定位
        /// </summary>
        AccuratelyPositioned,
        /// <summary>
        /// 车辆正在定位过程中
        /// </summary>
        PositioningProcess,
        /// <summary>
        /// 车辆失去定位
        /// </summary>
        LostLocation,
    }
}
