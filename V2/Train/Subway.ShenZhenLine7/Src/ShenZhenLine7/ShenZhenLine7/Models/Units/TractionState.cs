using System.ComponentModel;

namespace Subway.ShenZhenLine7.Models.Units
{
    /// <summary>
    /// 
    /// </summary>
    public enum TractionState
    {
        /// <summary>
        /// 牵引切除
        /// </summary>
        [Description("牵引切除")]
        Cut,
        /// <summary>
        /// 牵引激活
        /// </summary>
        [Description("牵引激活(加速/减速)，无故障")]
        Active,
        /// <summary>
        /// 牵引故障
        /// </summary>
        [Description("牵引故障")]
        Fault,
        /// <summary>
        /// 牵引断开
        /// </summary>
        [Description("牵引断开，无故障")]
        Off,
    }
}