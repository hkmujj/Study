using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 预告信息单元
    /// </summary>
    public interface IForecastInformationItem : INotifyPropertyChanged
    {
        /// <summary>
        /// 预告类型
        /// </summary>
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