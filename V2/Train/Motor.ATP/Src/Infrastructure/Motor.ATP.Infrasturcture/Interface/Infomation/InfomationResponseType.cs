using System.ComponentModel;

namespace Motor.ATP.Infrasturcture.Interface.Infomation
{
    /// <summary>
    /// 响应类型
    /// </summary>
    public enum InfomationResponseType
    {
        /// <summary>
        /// 无响应
        /// </summary>
        [Description("无响应")]
        None,

        /// <summary>
        /// 系统提供固定的弹出框
        /// </summary>
        [Description("系统")]
        System,

        /// <summary>
        /// 警惕确认, 200C适用
        /// </summary>
        [Description("警惕确认")]
        Vigilant,

        /// <summary>
        /// 缓解确认, 200C适用
        /// </summary>
        [Description("缓解确认")]
        Relase,

        /// <summary>
        /// 确认
        /// </summary>
        [Description("确认")]
        Ok,

        /// <summary>
        /// 确认取消
        /// </summary>
        [Description("确认取消")]
        OkCancel,

        /// <summary>
        /// 确认删除取消
        /// </summary>
        [Description("确认删除取消")]
        OkDelteCancel,
    }
}