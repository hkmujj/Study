using System.ComponentModel;

namespace Subway.ShiJiaZhuangLine1.Interface.Enum
{
    public enum BrakeModel
    {
        [Description("紧急制动")]
        EmergenctUnInfliction,
        [Description("紧急制动")]
        EmergencyInfliction,
        [Description("停放制动")]
        ParkingUninfliction,
        [Description("停放制动")]
        ParkingFliction,
        [Description("保持制动")]
        HoldBrake,
        [Description("快速制动")]
        FastBrake,
        [Description("牵引")]
        Traction,
        [Description("惰行")]
        Coasting,
        [Description("常用制动")]
        Braking,
    }
}