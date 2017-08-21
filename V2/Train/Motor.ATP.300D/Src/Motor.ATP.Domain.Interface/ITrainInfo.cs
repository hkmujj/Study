namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 车辆相关信息
    /// </summary>
    public interface ITrainInfo : IATPPartial
    {
        /// <summary>
        /// 速度信息
        /// </summary>
        IISpeed Speed { get; }

        /// <summary>
        /// 制动信息
        /// </summary>
        IBrake Brake { get; }

        /// <summary>
        /// 驾驶信息
        /// </summary>
        IDriver Driver { get; }

        /// <summary>
        /// 公里标
        /// </summary>
        IKilometerPost KilometerPost { get; }

    }
}