using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 转向架
    /// </summary>
    public interface IBogies : IPartialCar, IFaultable, IStateUpdatable, IListeningModelProvider
    {
        ReadOnlyCollection<IAxis> Axises { get; }
    }
}
