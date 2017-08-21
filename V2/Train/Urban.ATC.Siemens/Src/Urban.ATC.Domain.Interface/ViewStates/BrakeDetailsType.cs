using System.ComponentModel;

namespace Urban.ATC.Domain.Interface.ViewStates
{
    public enum BrakeDetailsType
    {
        [Description("初始状态")]
        Initial,

        [Description("要求制动")]
        BrakingRequired,

        [Description("紧急制动")]
        EnmergencyBrake,
    }
}