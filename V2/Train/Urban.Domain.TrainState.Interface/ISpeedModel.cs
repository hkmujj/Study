using System.ComponentModel;

namespace Urban.Domain.TrainState.Interface
{
    /// <summary>
    /// 速度模型
    /// </summary>
    public interface ISpeedModel : INotifyPropertyChanged
    {
        float Value { get; }
    }
}