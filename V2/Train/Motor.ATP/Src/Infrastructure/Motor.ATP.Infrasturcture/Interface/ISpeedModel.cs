using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 速度模型
    /// </summary>
    public interface ISpeedModel : INotifyPropertyChanged
    {
        /// <summary>
        /// 值
        /// </summary>
        float Value { get; set; }

        /// <summary>
        /// 颜色
        /// </summary>
        ATPColor SpeedColor { get; set; }
    }
}