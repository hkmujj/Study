using System.ComponentModel;

namespace Subway.ShiJiaZhuangLine1.Interface
{
    public enum EventLevel
    {
        [Description("")]
        Normal,
        [Description("严重故障")]
        Critical,
        [Description("中等故障")]
        Medium,
        [Description("轻微故障")]
        Light,
        [Description("事件信息")]
        Event,
    }
}