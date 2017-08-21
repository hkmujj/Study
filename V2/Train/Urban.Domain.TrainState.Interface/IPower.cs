using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    public interface IPower : IListeningModelProvider, IStateUpdatable
    {
        IContactLinePower ContactLinePower { get; }

        IAccumulatorPower AccumulatorPower { get; }
    }
}