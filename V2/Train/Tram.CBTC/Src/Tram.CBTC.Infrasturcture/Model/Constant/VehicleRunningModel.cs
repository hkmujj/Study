namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 车载运行模式
    /// 0、无
    /// 1、车载联机时刻表
    /// 2、车载联机等间隔
    /// 3、车载联机目的地
    /// 4、车载独立
    /// 5、手工控制
    /// </summary>
    public enum VehicleRunningModel
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 车载联机时刻表
        /// </summary>
        OnBoardOnlineTimetable,
        /// <summary>
        /// 车载联机等间隔
        /// </summary>
        VehicleOnlineEqualinterval,
        /// <summary>
        /// 车载联机目的地
        /// </summary>
        VehicleMountedOnlineDestination,
        /// <summary>
        /// 车载独立
        /// </summary>
        VehicleIndependent,
        /// <summary>
        /// 手工控制
        /// </summary>
        ManualControl,
        /// <summary>
        /// 车载联机，用于Pop窗口选择车载运行模式
        /// </summary>
        OnBoardOnline
    }
}
