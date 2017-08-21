using System.ComponentModel;

namespace Urban.Wuxi.TMS
{
    public enum HelpInfoMenu
    {
        [Description("紧急制动条件提示")]
        MenuOne,
        [Description("牵引封锁条件提示")]
        MenuTwo,
        [Description("限速")]
        MenuThree,
        [Description("紧急牵引")]
        MenuFour,
        [Description("其他")]
        MenuFive,
    }
}