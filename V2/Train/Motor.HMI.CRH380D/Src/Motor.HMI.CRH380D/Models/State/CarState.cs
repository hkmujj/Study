namespace Motor.HMI.CRH380D.Models.State
{
    /// <summary>
    ///     车辆状态
    /// </summary>
    public enum CarState
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        ///　牵引切除
        /// </summary>
        TractionCut,
        /// <summary>
        /// 牵引故障
        /// </summary>
        TractionFault,
        /// <summary>
        /// 牵引运行
        /// </summary>
        TractionTurnOn,
        /// <summary>
        /// 牵引未运行
        /// </summary>
        TractionTurnOff,
        /// <summary>
        /// 司机室舒适度切除
        /// </summary>
        CarComforCut,
        /// <summary>
        /// 转向架超温
        /// </summary>
        BogieOverHeat,
        /// <summary>
        /// 转向架正常
        /// </summary>
        BogieNormal,
        /// <summary>
        /// 制动试验施加
        /// </summary>
        BreakTestForce,
        /// <summary>
        /// 制动试验缓解
        /// </summary>
        BreakTestRemit,
        /// <summary>
        /// 制动试验故障
        /// </summary>
        BreakTestFault,
        /// <summary>
        /// 车辆车重正常
        /// </summary>
        StationNormal,
        /// <summary>
        /// 车辆超载
        /// </summary>
        StationOverLoad,
        /// <summary>
        /// 车辆严重超载
        /// </summary>
        StationSeriousOverLoad,
        /// <summary>
        /// 火灾探测器探测到发生火灾
        /// </summary>
        FireDeviceFireHappened,
        /// <summary>
        /// 火灾探测器正常
        /// </summary>
        FireDeviceNormal,
    }
}