namespace Motor.ATP.Domain.Interface
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
        /// 车次号
        /// </summary>
        string TrainId { set; get; }

        /// <summary>
        /// 车次号是否可见
        /// </summary>
        bool TrainIdVisible { get; }
    }
}