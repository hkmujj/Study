using System.ComponentModel;

namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    ///     空调控制模式
    /// </summary>
    public enum AirControlModel
    {
        /// <summary>
        ///     默认
        /// </summary>
        [Description("")]
        Normal,

        /// <summary>
        ///     本控
        /// </summary>
        [Description("本控")]
        CurrentControl,

        /// <summary>
        ///     集控
        /// </summary>
        [Description("集控")]
        CollectCOntrol
    }
}