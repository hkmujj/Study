namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 数据协议
    /// </summary>
    public interface INetDataProtocolConfig
    {
        /// <summary>
        /// 协议类型
        /// </summary>
        NetDataProtocolType ProtocolType { get; }

        /// <summary>
        /// 只有包号的配置
        /// </summary>
        IPackageIdOnlyConfig PackageIdOnlyConfig { get; }

        /// <summary>
        /// 业务id和包号的配置
        /// </summary>
        IBussnessIdAndPackageIdConfig BussnessIdAndPackageIdConfig { get; }
    }
}