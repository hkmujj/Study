namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 车轮
    /// </summary>
    public interface IWheel : IFaultable, IIdentityProvide, ITemperatureMensurability
    {
        /// <summary>
        /// 空转次数
        /// </summary>
        uint Iding { get; }

        /// <summary>
        /// 滑行次数
        /// </summary>
        uint Slide { get; }
    }
}
