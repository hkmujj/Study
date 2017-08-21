using System.Collections.ObjectModel;
using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    public interface ICarBraking : ICanCutPart, IPartialCar, IStateUpdatable, IListeningModelProvider
    {
         ReadOnlyCollection<IBraking> Brakings { get; }
    }
}