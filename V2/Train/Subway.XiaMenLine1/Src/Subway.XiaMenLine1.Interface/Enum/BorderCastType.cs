using System.ComponentModel;

namespace Subway.XiaMenLine1.Interface.Enum
{
    public enum BorderCastType
    {
        [Description("上一站跳站")]
        LastSkip,
        [Description("下一站跳站")]
        NextSkip,
        [Description("到站广播")]
        Arrive,
        [Description("离站广播")]
        DeArrive,
        [Description("越站")]
        Skip
    }
}