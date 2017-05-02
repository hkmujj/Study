namespace Subway.WuHanLine6.Models.States
{
    /// <summary>
    ///     压缩机 冷凝机  通风机
    /// </summary>
    public enum ChanceType
    {
        /// <summary>
        ///     压缩机
        /// </summary>
        Compress = 1,

        /// <summary>
        ///     冷凝机
        /// </summary>
        Condenstate = 2,

        /// <summary>
        ///     通风机
        /// </summary>
        Ventlate = 3,

        /// <summary>
        ///     课室加热
        /// </summary>
        Heat = 4
    }
}