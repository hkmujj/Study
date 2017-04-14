using MMI.Facility.Interface.Data.Config.NetDataPackage;

namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 只有包号的配置
    /// </summary>
    public interface IPackageIdOnlyConfig
    {
        /// <summary>
        /// 数据配置
        /// </summary>
        INetDataConfig NetDataConfig { get; }
    }
}