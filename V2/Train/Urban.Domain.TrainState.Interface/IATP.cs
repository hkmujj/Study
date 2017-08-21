using Urban.Domain.TrainState.Interface.Common;

namespace Urban.Domain.TrainState.Interface
{
    public interface IATP : IStateUpdatable, IListeningModelProvider
    {
        object ATPState { get; }

        object WorkModel { get; }
    }
}