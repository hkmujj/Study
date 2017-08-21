using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 受电弓 
    /// </summary>
    public interface IPantograph : IFaultable, IPartialCar, ICanCutPart, INameProvider, IListeningModelProvider, IStateUpdatable
    {
        PantographState State { get; }

    }

    /// <summary>
    /// 受电弓状态
    /// </summary>
    public enum PantographState
    {
        Up,
        Down,
        Unkown,
    }
}