namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 网络通道配置
    /// </summary>
    public interface INetChannelConfig
    {
        /// <summary>
        /// 选择的网络类型
        /// </summary>
        NetType NetType { get; }

        /// <summary>
        /// A 类具体配置
        /// </summary>
        IANetConfig ANetConfig { get; }

        /// <summary>
        /// B 类具体配置
        /// </summary>
        IBNetConfig BNetConfig { get; }

        /// <summary>
        /// C 类具体配置
        /// </summary>
        ICNetConfig CNetConfig { get; }

        /// <summary>
        /// 
        /// </summary>
        IPTT2DNetConfig PTT2DNetConfig { get; }
    }
}