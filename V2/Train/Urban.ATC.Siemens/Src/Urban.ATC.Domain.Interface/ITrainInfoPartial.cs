using System.ComponentModel;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 车辆信息的一部分
    /// </summary>
    public interface ITrainInfoPartial : INotifyPropertyChanged
    {
        ITrainInfo Parent { get; }
    }
}