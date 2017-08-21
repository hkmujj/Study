namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 网络配置
    /// </summary>
    public interface INetConfig
    {
        /// <summary>
        /// 网络通道配置
        /// </summary>
        INetChannelConfig NetChannelConfig { get; }

        /// <summary>
        /// 网络数据协议配置
        /// </summary>
        INetDataProtocolConfig NetDataProtocolConfig { get; }
    }
}