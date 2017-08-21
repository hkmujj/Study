namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 列车所在区域
    /// 0、初始化状态 
    /// 1、停车对标 
    /// 2、发车 
    /// 3、区间 
    /// 4、站台 
    /// 5、车场 
    /// 6、转换轨
    /// 7、折返轨
    /// </summary>
    public enum TrainPosition
    {
        /// <summary>
        /// 初始化状态
        /// </summary>
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
        /// 车场
        /// </summary>
        CarDepot,

        /// <summary>
        /// 转换轨
        /// </summary>
        TransferRail,

        /// <summary>
        /// 折返轨
        /// </summary>
        ReturnRail,

      
    }
}
