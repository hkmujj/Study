using System.ComponentModel;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum AirConditionState
    {
        [Description("空调运行，无故障")]
        Normal,
        [Description("")]
        Select,
        [Description("")]
        Fault
    }
}