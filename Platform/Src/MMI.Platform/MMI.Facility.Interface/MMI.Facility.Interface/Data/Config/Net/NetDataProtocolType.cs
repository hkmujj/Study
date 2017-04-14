namespace MMI.Facility.Interface.Data.Config.Net
{
    /// <summary>
    /// 数据协议类型
    /// </summary>
    public enum NetDataProtocolType
    {
        /// <summary>
        /// 只有包号
        /// </summary>
        PackageIdOnly,

        /// <summary>
        /// 业务ID和包号
        /// </summary>
        BussnessIdAndPackageId,
    }
}