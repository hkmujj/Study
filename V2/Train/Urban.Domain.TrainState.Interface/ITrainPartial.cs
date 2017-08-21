using System.ComponentModel;

namespace Urban.Domain.TrainState.Interface
{
    public interface ITrainPartial : INotifyPropertyChanged
    {
        ITrain Parent { get; }
    }
}