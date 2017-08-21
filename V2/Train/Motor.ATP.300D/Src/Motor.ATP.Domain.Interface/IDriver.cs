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
        string TrainLine { set; get; }
    }
}