using System.ComponentModel;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 速度模型
    /// </summary>
    public interface ISpeedModel : INotifyPropertyChanged
    {
        float Value { get; set; }

        ATPColor SpeedColor { get; set; }
    }
}