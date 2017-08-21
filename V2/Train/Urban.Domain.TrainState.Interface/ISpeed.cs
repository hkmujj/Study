namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 速度信息
    /// </summary>
    public interface ISpeed : IVisibility
    {
        /// <summary>
        /// 当前速度
        /// </summary>
        ISpeedModel CurrentSpeed { get; }

        /// <summary>
        /// 限制速度 
        /// </summary>
        ISpeedModel LimitedSpeed { get; }

    }
}