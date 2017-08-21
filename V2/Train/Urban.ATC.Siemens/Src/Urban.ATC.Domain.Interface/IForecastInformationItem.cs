using System.ComponentModel;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 预告信息单元
    /// </summary>
    public interface IForecastInformationItem : INotifyPropertyChanged
    {
        ForecastInformationType Type { get; }

        /// <summary>
        /// 距离， 以1为单位
        /// </summary>
        float Distance {  get; }

        /// <summary>
        /// 更新数据
        /// </summary>
        /// <param name="type"></param>
        /// <param name="distance"></param>
        void Update(ForecastInformationType type, float distance);
    }
}