using System.ComponentModel;

namespace Motor.ATP.Domain.Interface.SpeedMonitoringSection
{
    public interface IGradientInfomationItem : INotifyPropertyChanged
    {
        /// <summary>
        /// 父结点
        /// </summary>
        IGradientInfomation Parent { get; }

        /// <summary>
        /// 坡度类型
        /// </summary>
        GradientType Type { set; get; }

        /// <summary>
        /// 坡度值
        /// </summary>
        float SlopeValue { set; get; }

        /// <summary>
        /// 坡度值
        /// </summary>
        float AbsSlopeValue { get; }

        /// <summary>
        /// 起始距离
        /// </summary>
        float StartDistance { set; get; }

        /// <summary>
        /// 结束距离 
        /// </summary>
        float EndDistance { set; get; }
    }
}