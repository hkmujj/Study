namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 列车位置
    /// </summary>
    public enum TrainPosition
    {
        Initalize,

        /// <summary>
        /// 停车对标
        /// </summary>
        ParkingBenchmarking,

        /// <summary>
        /// 发车
        /// </summary>
        Depart,

        /// <summary>
        /// 区间运行
        /// </summary>
        InSection,

        /// <summary>
        /// 站台运行
        /// </summary>
        InStation,

        /// <summary>
        /// 转换轨
        /// </summary>
        TransferRail,

        /// <summary>
        /// 折返轨
        /// </summary>
        ReturnRail,

        /// <summary>
        /// 车辆段
        /// </summary>
        CarDepot,
    }
}