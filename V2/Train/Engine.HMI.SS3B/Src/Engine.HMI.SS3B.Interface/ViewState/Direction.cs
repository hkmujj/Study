using System.ComponentModel;

namespace Engine.HMI.SS3B.Interface.ViewState
{
    public enum Direction
    {
        [Description("未  知")]
        Unknow,
        [Description("向  前")]
        Up,
        [Description("向  后")]
        Down,
    }
}