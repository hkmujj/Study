using System.ComponentModel;

namespace Engine.HMI.SS3B.Interface.ViewState
{
    public enum WorkModle
    {
        [Description("未  知")]
        None=0,
        [Description("主断合")]
        MasterClose=1,
        [Description("制  动")]
        Brake=2,
        [Description("牵引")]
        Taction=3,
        [Description("主断分")]
        MasterOpen = 4,
    }
}