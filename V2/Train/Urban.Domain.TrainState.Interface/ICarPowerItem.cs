using Urban.Domain.TrainState.Interface.Common;
using Urban.Domain.TrainState.Interface.Statues;

namespace Urban.Domain.TrainState.Interface
{
    public interface ICarPowerItem : IListeningModelProvider, IStateUpdatable, ISateProvider<CarPowerState>,
        ITypeProvider<CarPowerType>
    {
    }
}