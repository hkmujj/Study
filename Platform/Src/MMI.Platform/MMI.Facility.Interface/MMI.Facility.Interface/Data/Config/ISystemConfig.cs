using System.Collections.ObjectModel;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISystemConfig
    {
        /// <summary>
        /// 是否为调试
        /// </summary>
        bool IsDebugModel { get; }

        /// <summary>
        /// 启动模式
        /// </summary>
        StartModel StartModel { get; }


        /// <summary>
        /// 子系统集合
        /// </summary>
        ReadOnlyCollection<ISubsystemConfig> SubsystemConfigCollection { get; }
    }
}
