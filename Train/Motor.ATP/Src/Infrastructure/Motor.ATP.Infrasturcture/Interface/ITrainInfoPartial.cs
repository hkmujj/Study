using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 车辆信息的一部分
    /// </summary>
    public interface ITrainInfoPartial : INotifyPropertyChanged
    {
        /// <summary>
        /// 
        /// </summary>
        ITrainInfo Parent { get; }
    }
}