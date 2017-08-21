using System.ComponentModel;

namespace Motor.ATP.Domain.Interface.Infomation
{
    /// <summary>
    /// 响应类型
    /// </summary>
    public enum InfomationResponseType
    {
        [Description("无响应")]
        None,

        [Description("系统")]
        System,

        [Description("确认")]
        Ok,

        [Description("确认取消")]
        OkCancel,

        [Description("确认删除取消")]
        OkDelteCancel,
    }
}