using System.Collections.ObjectModel;
using MMI.Facility.Interface.Data.Config.Net;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IConfig
    {
        /// <summary>
        /// 系统目录 
        /// </summary>
        ISystemDicrectory SystemDicrectory { get; }

        /// <summary>
        /// 
        /// </summary>
        IIndexDescriptionConfig IndexDescriptionConfig { get; }

        /// <summary>
        /// 启动配置
        /// </summary>
        ISystemConfig SystemConfig { get; }

        /// <summary>
        /// 所有的屏的关联关系
        /// </summary>
        IScreenTableConfig ScreenTableConfig { get; }

        /// <summary>
        /// 网络配置
        /// </summary>
        INetConfig NetConfig { get; }

        /// <summary>
        /// 调试窗体配置
        /// </summary>
        IDebugWindowConfig DebugWindowConfig { get; }

        /// <summary>
        /// 所有的运行时配置
        /// </summary>
        ReadOnlyCollection<IAppConfig> AppConfigs { get; }


    }
}
