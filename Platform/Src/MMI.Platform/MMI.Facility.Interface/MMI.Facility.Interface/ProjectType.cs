namespace MMI.Facility.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public enum ProjectType : byte
    {
        /// <summary>
        /// 
        /// </summary>
        Unkown = 0,

        /// <summary>
        /// 信号
        /// </summary>
        Signal = 1,

        /// <summary>
        /// 车辆
        /// </summary>
        Train,

        /// <summary>
        /// VT
        /// </summary>
        VT,

        /// <summary>
        /// 气表盘
        /// </summary>
        DialPlate,

        /// <summary>
        /// 电台
        /// </summary>
        Radio,

        /// <summary>
        /// 动态地图
        /// </summary>
        DynamicMap,
    }
}