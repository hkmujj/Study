using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    public interface ICarPower : IListeningModelProvider, IStateUpdatable
    {
        ReadOnlyCollection<ICarPowerItem> PowerItems { get; }
    }
}