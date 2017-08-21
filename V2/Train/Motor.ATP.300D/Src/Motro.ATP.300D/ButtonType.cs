using System.ComponentModel;

namespace Motor.ATP._300D
{
    public enum ButtonType
    {
        [Description("未知")]
        None = -1,

        [Description("调车\n1")]
        B1 = 0,
        [Description("目视\n2")]
        B2,
        [Description("机信\n3")]
        B3,
        [Description("启动\n4")]
        B4,
        [Description("缓解\n5")]
        B5,
        [Description("6")]
        B6,
        [Description("7")]
        B7,
        [Description("8")]
        B8,
        [Description("9")]
        B9,
        [Description("0")]
        B0,
        [Description("字母\n数字")]
        BSwitch,
        [Description("F1")]
        F1,
        [Description("F2")]
        F2,
        [Description("F3")]
        F3,
        [Description("F4")]
        F4,
        [Description("F5")]
        F5,
        [Description("F6")]
        F6,
        [Description("F7")]
        F7,
        [Description("F8")]
        F8,

        /// <summary>
        /// ATP 确认
        /// </summary>
        [Description("ATP\n确认")]
        ATPConfirm,
    }
}