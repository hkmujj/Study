namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 预告信息类型
    /// </summary>
    public enum ForecastInformationType
    {
        /// <summary>
        /// 无预告信息
        /// </summary>
        None,

        /// <summary>
        /// 车站
        /// </summary>
        Station,

        /// <summary>
        /// 隧道
        /// </summary>
        Tunnel,

        /// <summary>
        /// 临时限制速度区
        /// </summary>
        TemporarySpeedLimit ,

        /// <summary>
        /// 分相区灰色
        /// </summary>
        PhaseSeparatingSection,

        /// <summary>
        /// 分相区黄色
        /// </summary>
        PhaseSeparatingSectionYellow,

        /// <summary>
        /// 桥梁
        /// </summary>
        Bridge,

        /// <summary>
        /// 隔离
        /// </summary>
        Isolated,
    }
}