using System.ComponentModel;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum ControlModel
    {
        [Description("未知")]
        UnKnow,
        [Description("URM")]
        URM,
        [Description("ATO")]
        ATO,
        [Description("SM")]
        SM,
        [Description("RM")]
        RM,
        [Description("AR")]
        AR,
        [Description("ATP切除")]
        ATPCut,
        [Description("PM")]
        PM,
    }
}