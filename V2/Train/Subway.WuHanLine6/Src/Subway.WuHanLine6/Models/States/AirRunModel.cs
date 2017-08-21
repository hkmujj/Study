using System.ComponentModel;

namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    ///     空调运行模式
    /// </summary>
    public enum AirRunModel
    {
        /// <summary>
        ///     默认
        /// </summary>
        [Description("")]
        Normal,

        /// <summary>
        ///     自动制冷
        /// </summary>
        [Description("自动")]
        Auto
    }
}