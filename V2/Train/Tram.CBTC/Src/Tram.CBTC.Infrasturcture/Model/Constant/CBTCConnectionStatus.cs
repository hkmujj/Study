namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// CBTC连接状态
    /// 0、无
    /// 1、无连接 
    /// 2、正在建立一条连接 
    /// 3、正在建立两条连接 
    /// 4、一条建立  
    /// 5、一条建立，一条没有建立 
    /// 6、两条建立
    /// 7、建立连接
    /// </summary>
    public enum CBTCConnectionStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// 无连接
        /// </summary>
        NoConnection,
        /// <summary>
        /// 正在建立一条连接
        /// </summary>
        OneConnection,
        /// <summary>
        /// 正在建立两条连接
        /// </summary>
        TwoConnection,
        /// <summary>
        /// 一条建立
        /// </summary>
        OneConnected,
        /// <summary>
        /// 一条建立，一条没有建立 
        /// </summary>
        OneConnectedOneNotConnected,
        /// <summary>
        /// 两条建立
        /// </summary>
        TwoConnected,
        /// <summary>
        /// 建立连接
        /// </summary>
        Connected,
    }
}
