using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 制动
    /// </summary>
    public interface IBraking : IStateUpdatable, IListeningModelProvider
    {
        BrakingType BrakingType { get; }

        BrakingState BrakingState { get; }

        BrakingLevel BrakingLevel { get; }

        /// <summary>
        /// 制动值
        /// </summary>
        float Value { get; }
    }
}