using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface.SpeedMonitoringSection
{
    /// <summary>
    /// 速度变化信息
    /// </summary>
    public interface ISpeedChangeInfo: INotifyPropertyChanged
    {
        /// <summary>
        /// 减速类型
        /// </summary>
        SpeedChangeType SpeedChangeType { set;get; }

        /// <summary>
        /// 距离
        /// </summary>
        double Distance { get; set; }
    }
}