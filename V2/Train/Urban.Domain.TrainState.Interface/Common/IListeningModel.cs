using Mmi.Communication.Index.Adapter;

namespace Urban.Domain.TrainState.Interface.Common
{
    public interface IListeningModel : INameProvider
    {
        CommunicationIndexType Type { get; }
    }
}