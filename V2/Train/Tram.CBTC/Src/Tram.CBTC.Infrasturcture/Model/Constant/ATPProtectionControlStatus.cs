namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// ATP保护控制状态
    /// 0、无
    /// 1、ATP保护功能激活
    /// 2、ATP保护功能未激活
    /// 3、授权以低速越过禁止信号
    /// 4、牵引制动信息异常
    /// 5、里程计异常
    /// </summary>
    public enum ATPProtectionControlStatus
    {
        /// <summary>
        /// 无
        /// </summary>
        None,
        /// <summary>
        /// ATP保护功能激活
        /// </summary>
        ATPProtectActive,
        /// <summary>
        /// ATP保护功能未激活
        /// </summary>
        ATPProtectUnActive,
        /// <summary>
        /// 授权以低速越过禁止信号
        /// </summary>
        AuthorizeToPassTheInhibitSignalAtLowSpeed,
        /// <summary>
        /// 牵引制动信息异常
        /// </summary>
        TractionBrakeInformationAbnormality,
        /// <summary>
        /// 里程计异常
        /// </summary>
        OdometerAnomaly,
    }
}
