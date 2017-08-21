namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 驾驶信息
    /// </summary>
    public interface IDriver : ITrainInfoPartial
    {
        /// <summary>
        /// 司机号
        /// </summary>
        string DriverId { set; get; }

        /// <summary>
        /// 正在输入的司机号
        /// </summary>
        string InputtingDriverId { set; get; }

        /// <summary>
        /// 是否正在输入司机号
        /// </summary>
        bool IsInputtingDriverId { set; get; }

        /// <summary>
        /// 车次号
        /// </summary>
        string TrainId { set; get; }

        /// <summary>
        /// 正在输入的车次号
        /// </summary>
        string InputtingTrainId { set; get; }

        /// <summary>
        /// 是否正在输入车次号
        /// </summary>
        bool IsInputtingTrainId { set; get; }

        /// <summary>
        /// 车次号是否可见
        /// </summary>
        bool TrainIdVisible { get; }
    }
}