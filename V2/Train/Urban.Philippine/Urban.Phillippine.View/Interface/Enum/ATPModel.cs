using System.ComponentModel;

namespace Urban.Phillippine.View.Interface.Enum
{
    public enum AtpModel
    {
        [Description("ATP Mode")]
        AtpMode = 1,


        [Description("EMERGENCY Mode")]
        Emergency = 2,


        [Description("MANUAL Forward Mode")]
        ManualForward = 3,

        [Description("MANUAL Backward Mode")]
        ManualBackward = 4,

        [Description("IL")]
        Il = 5
    }
}