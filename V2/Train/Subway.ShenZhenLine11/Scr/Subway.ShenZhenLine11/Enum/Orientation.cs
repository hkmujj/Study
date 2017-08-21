using System.ComponentModel;

namespace Subway.ShenZhenLine11.Enum
{
    public enum Orientation
    {
        [Description("未知")]
        UnKnow = 0,

        [Description("向前")]
        Before = 1,

        [Description("零位")]
        Zero = 2,

        [Description("向后")]
        Back = 3,

    }

    public enum RunModel
    {
        [Description("ATO")]
        ATO = 1,
        [Description("AMC")]
        AMC = 2,
        [Description("MCS")]
        MCS = 3,
        [Description("RM")]
        RM = 4,
        [Description("NRM")]
        NRM = 5,
        [Description("ATB")]
        ATB = 6,
        [Description("BM")]
        BM = 7,
        [Description("ATP切除")]
        ATPCut = 8,
        [Description("Wash")]
        Wash,
        [Description("预留")]
        Reserve1,
        [Description("预留")]
        Reserve2,
        [Description("未知")]
        UnKnow,
    }

    public enum RunWork
    {
        [Description("牵引")]
        Traction = 1,

        [Description("制动")]
        Brake,
        [Description("快速制动")]
        FastBrake,
        [Description("紧急制动")]
        EmergencyBrake,
        [Description("惰行")]
        Coasting,
        [Description("停放制动")]
        ParkingBrake,
        [Description("紧急牵引")]
        EmergencyTraction,
        [Description("保持制动")]
        KeepBrake,
        [Description("预留")]
        Reserve2,
        [Description("未知")]
        UnKnow,

    }

    public enum BorderCastModel
    {
        [Description("自动")]
        Auto = 1,

        [Description("手动")]
        Manual = 2,
    }
}