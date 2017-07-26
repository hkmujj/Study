namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 制动信息
    /// </summary>
    public interface IBrake : ITrainInfoPartial, IVisibility
    {
        /// <summary>
        /// 制动类型
        /// </summary>
        BrakeType BrakeType { get; }

        /// <summary>
        /// 可见性
        /// </summary>
        bool Visible { set; get; }
    }
}