using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 风缸
    /// </summary>
    public interface IAirCylinder : IListeningModelProvider, IStateUpdatable
    {
        /// <summary>
        /// 总风缸压力
        /// </summary>
        float TotalPressure { get; }

        /// <summary>
        /// 制动缸压力
        /// </summary>
        float BrakePressure { get; }
    }
}