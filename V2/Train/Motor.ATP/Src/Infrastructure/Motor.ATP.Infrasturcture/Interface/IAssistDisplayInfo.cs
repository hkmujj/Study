using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 辅显信息
    /// </summary>
    public interface IAssistDisplayInfo : INotifyPropertyChanged
    {
        /// <summary>
        /// 是否显示辅显信息
        /// </summary>
        bool Visible { get; }
        /// <summary>
        /// 当前速度
        /// </summary>

        string CurrentSpeed { get; }
        /// <summary>
        /// 限制速度
        /// </summary>

        string LimitedSpeed { get; }
        /// <summary>
        /// 目标速度
        /// </summary>
        string CabSignalCodeTargetSpeedPair { get; }
        /// <summary>
        /// 目标距离
        /// </summary>
        string TargetDistance { get; }
        /// <summary>
        /// 载频上行
        /// </summary>
        bool FrequencyUp { get; set; }
        /// <summary>
        /// 载频下行
        /// </summary>
        bool FrequencyDown { get; set; }
        /// <summary>
        /// 分相有效
        /// </summary>
        bool SplitPhaseEnable { get; set; }
        /// <summary>
        /// 分相执行
        /// </summary>
        bool SplitPhaseExecute { get; set; }
    }
}