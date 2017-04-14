namespace MMI.Facility.Interface.Data.Config
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISystemDicrectory
    {
        /// <summary>
        /// 程序所在目录 
        /// </summary>
        string BaseDirectory { get; }

        /// <summary>
        /// 插件目录 
        /// </summary>
        string AddinDirectory { get; }

        /// <summary>
        /// 系统配置目录 
        /// </summary>
        string SystemConfigDirectory { get; }

        /// <summary>
        /// 系统资源目录 
        /// </summary>
        string SystemResourceDirectory { get; }
    }
}
