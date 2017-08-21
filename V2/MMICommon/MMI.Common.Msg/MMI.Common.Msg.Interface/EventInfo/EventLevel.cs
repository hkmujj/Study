using System.ComponentModel;

namespace MMI.Common.Msg.Interface.EventInfo
{
    public enum EventLevel
    {
        [Description("")]
        Normal,
        [Description("严重")]
        Critical,
        [Description("中级")]
        Medium,
        [Description("轻微")]
        Light
    }
}