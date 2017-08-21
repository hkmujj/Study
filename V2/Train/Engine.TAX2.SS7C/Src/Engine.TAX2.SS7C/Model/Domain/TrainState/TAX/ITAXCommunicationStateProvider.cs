using System.ComponentModel;
using Engine.TAX2.SS7C.Model.Domain.Constant;

namespace Engine.TAX2.SS7C.Model.Domain.TrainState.TAX
{
    public interface ITAXCommunicationStateProvider : INotifyPropertyChanged
    {
        TAX2CommunicationState TAX2CommunicationState { get; }
    }
}