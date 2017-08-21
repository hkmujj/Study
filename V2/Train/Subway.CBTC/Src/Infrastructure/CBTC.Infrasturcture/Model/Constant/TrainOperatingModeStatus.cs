namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// Crate User：tandw
    /// Crate Time：2017/06/16
    /// 根据深圳地铁9号线 泰雷兹信息屏资料编写
    /// 当前驾驶模式状态  适用于 ATP  IATP ATO 模式
    /// </summary>
    public enum TrainOperatingModeStatus
    {
        /// <summary>
        /// 对移动的列车可用
        /// </summary>
        AvailableForMoveTrain,
        /// <summary>
        /// 对停止的列车可用
        /// </summary>
        AvailableForStationaryTrain,
        /// <summary>
        /// 不可用
        /// </summary>
        NotAvailable
    }
}
