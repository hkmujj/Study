using MMI.Facility.Interface.Data.Config.Form;

namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface IAppConfig
    {
        /// <summary>
        /// 工程名
        /// </summary>
        string AppName { get; }

        /// <summary>
        /// 工程类型
        /// </summary>
        ProjectType ProjectType { get; }

        /// <summary>
        /// 
        /// </summary>
        SubsystemType SubsystemType { get; }

        /// <summary>
        /// 
        /// </summary>
        ISubsystemConfig SubsystemConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        AppPaths AppPaths { get; }

        /// <summary>
        /// 
        /// </summary>
        IConfig ParentConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        IAppLogicConfigCollection AppLogicConfig { get; }

        /// <summary>
        /// 通信数据接口配置
        /// </summary>
        IAppCommunicationInterfaceConfig AppCommunicationInterfaceConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        IAppViewConfig AppViewConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        IAppObjcetConfig AppObjcetConfig { get; }

        /// <summary>
        /// 文件配置
        /// </summary>
        IAppFileConfig AppFileConfig { get; }

        /// <summary>
        /// 实际显示窗体配置
        /// </summary>
        IActureFormConfig ActureFormConfig { get; }

        /// <summary>
        /// 显示窗口的显示配置
        /// </summary>
        IActureFormViewConfig ActureFormViewConfig { get; }
    }
}
