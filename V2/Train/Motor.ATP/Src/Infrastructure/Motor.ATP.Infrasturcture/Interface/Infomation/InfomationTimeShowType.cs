using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
{
    /// <summary>
    /// 时间显示类型
    /// </summary>
    public enum InfomationTimeShowType
    {
        /// <summary>
        /// 普通文本显示
        /// </summary>
        [Description("静态显示发生时间")]
        Normal,

        /// <summary>
        /// 静态显示0：0：0
        /// </summary>
        [Description("静态显示0")]
        Static0,

        /// <summary>
        /// 从0开始计时
        /// </summary>
        [Description("从0开始计时")]
        TimeFrom0,

        /// <summary>
        /// 从发生时间开始计时
        /// </summary>
        [Description("从发生时间开始计时")]
        TimeFromOccuse,

        /// <summary>
        /// 不显示时间
        /// </summary>
        [Description("不显示时间")]
        NoTime,
    }
}