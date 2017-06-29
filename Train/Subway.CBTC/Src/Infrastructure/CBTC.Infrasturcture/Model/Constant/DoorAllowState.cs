namespace CBTC.Infrasturcture.Model.Constant
{
    public enum DoorAllowState
    {
        /// <summary>
        /// 
        /// </summary>
        Unkown,

        ///// <summary>
        ///// 被旁路
        ///// </summary>
        //Bypassed,

        /// <summary>
        /// 不允许
        /// </summary>
        NoAllow,

        /// <summary>
        /// 允许开同时两侧
        /// </summary>
        AllowBoth,

        /// <summary>
        /// 只能开左
        /// </summary>
        LeftAllow,

        /// <summary>
        /// 只能开右
        /// </summary>
        RightAllow,

        /// <summary>
        /// 先开左，再开右
        /// </summary>
        FirstLeft,

        /// <summary>
        /// 先开右，再开左
        /// </summary>
        FirstRight,
    }
}