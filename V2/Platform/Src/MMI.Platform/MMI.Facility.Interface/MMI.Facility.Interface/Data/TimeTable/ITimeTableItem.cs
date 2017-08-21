namespace MMI.Facility.Interface.Data.TimeTable
{
    /// <summary>
    /// 时刻表项
    /// </summary>
    public interface ITimeTableItem
    {
        /// <summary>
        /// 站点ID
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// 发车时间
        /// </summary>
        string DepartTime { get; set; }

        /// <summary>
        /// 到站时间
        /// </summary>
        string ArriveTime { get; set; }
    }
}
