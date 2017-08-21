namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 公里标
    /// </summary>
    public interface IKilometerPost : ITrainInfoPartial, IVisibility
    {
        /// <summary>
        /// 千米
        /// </summary>
        double Kilometer { get; }

        /// <summary>
        /// 米
        /// </summary>
        double Meter { get; }

    }
}
