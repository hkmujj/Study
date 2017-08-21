using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 车门
    /// </summary>
    public interface IDoor : IPartialCar, IFaultable, ICanCutPart, IListeningModelProvider, IStateUpdatable,
        INameProvider, IIdentityProvide
    {
        /// <summary>
        /// 车门状态
        /// </summary>
        DoorState DoorState { get; }
    }

    public enum DoorState
    {
        Unkown,

        Open,

        Close,

        Fault,

        Lock,

        Unlock,

        EmergencyUnlock,

        /// <summary>
        ///  有障碍物
        /// </summary>
        Obstacle,

        CutOut
    }
}
