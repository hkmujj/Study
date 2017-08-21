namespace CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 当前驾驶模式
    /// </summary>
    public enum TrainOperatingMode
    {
        /// <summary>
        /// 不显示
        /// </summary>
        Normal,

        /// <summary>
        /// RM、RMF
        /// </summary>
        RM,

        /// <summary>
        /// CM、SM、ATP、ATPM、MCS模式
        /// </summary>
        CM,

        /// <summary>
        /// AM、ATO、AMC模式
        /// </summary>
        AM,

        /// <summary>
        /// NRM、EUM
        /// </summary>
        NRM,

        /// <summary>
        ///待机模式
        /// </summary>
        SB,

        /// <summary>
        /// RMR模式运行
        /// </summary>
        RMR,

        /// <summary>
        /// ATB、DTB
        /// </summary>
        ATB,

        /// <summary>
        /// OFF 关断模式
        /// </summary>
        OFF,

        /// <summary>
        /// WASH 模式运行
        /// </summary>
        WASH,

        /// <summary>
        /// IATO、AM-I 、IAM  点式ATO模式运行
        /// </summary>
        IATO,

        /// <summary>
        /// IATP 点式ATP模式运行
        /// </summary>
        IATP,

        /// <summary>
        /// SHUNT 调车模式
        /// </summary>
        SHUNT

        ///// <summary>
        ///// 无人驾驶自动折返
        ///// </summary>
        //DTB,
        ///// <summary>
        ///// AMC模式
        ///// </summary>
        //AMC,
        ///// <summary>
        ///// MCS模式
        ///// </summary>
        //MCS,


    }
}