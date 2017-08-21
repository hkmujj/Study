using System.ComponentModel;
using Microsoft.Practices.Prism.Events;

namespace Engine._6A.Event
{
    public class ButtonEvent : CompositePresentationEvent<ButttonType>
    {

    }

    public enum ButttonType
    {
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
        [Description("上")]
        Up,
        [Description("下")]
        Down,
        [Description("左")]
        Left,
        [Description("右")]
        Right,
        [Description("第一排确认")]
        OneConfirm,
        [Description("下一页")]
        Next,
        [Description("上一页")]
        Last,
        [Description("测试")]
        Test,
        [Description("第二排确认")]
        TwoConfirm,

    }
}