namespace Tram.CBTC.Infrasturcture.Model.Constant
{
    /// <summary>
    /// 车门允许状态
    /// 0、无效
    /// 1、门旁路 
    /// 2、不允许开门 
    /// 3、允许车门开启 
    /// 4、允许车门先开启
    /// 5、允许车门后开启
    /// </summary>
    public enum DoorAllowStatus
    {
        /// <summary>
        /// 无效
        /// </summary>
        Invalid,
        /// <summary>
        /// 门旁路
        /// </summary>
        Bypass,
        /// <summary>
        /// 不允许开门
        /// </summary>
        NotAllowDoorOpen,
        /// <summary>
        /// 允许车门开启
        /// </summary>
        AllowDoorOpen,
        /// <summary>
        /// 允许车门先开启
        /// </summary>
        AllowDoorBeforeOpen,
        /// <summary>
        /// 允许车门后开启
        /// </summary>
        AllowDoorAfterOpen,

    }
}
