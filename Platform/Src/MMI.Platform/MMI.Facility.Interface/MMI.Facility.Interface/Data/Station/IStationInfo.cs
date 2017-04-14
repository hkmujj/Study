namespace MMI.Facility.Interface.Data.Station
{
    /// <summary>
    /// 车站信息
    /// </summary>
    public interface IStationInfo
    {
        /// <summary>
        /// 车站索引
        /// </summary>
        int Index { get; }

        /// <summary>
        /// 车站名
        /// </summary>
        string Name { get; }
    }
}