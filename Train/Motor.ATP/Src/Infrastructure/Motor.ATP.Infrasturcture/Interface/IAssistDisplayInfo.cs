using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 辅显信息
    /// </summary>
    public interface IAssistDisplayInfo : INotifyPropertyChanged
    {
        bool Visible { get; }

        string CurrentSpeed { get; }

        string LimitedSpeed { get; }

        string CabSignalCodeTargetSpeedPair { get; }

        string TargetDistance { get; }
    }
}