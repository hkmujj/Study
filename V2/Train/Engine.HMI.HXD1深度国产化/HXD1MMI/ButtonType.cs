using System.ComponentModel;
using System.Diagnostics;
using CommonUtil.Controls;

namespace HXD1.DeepDomestic
{
    public enum ButtonType
    {
        [Description("默认")]
        None,
        [Description("1")]
        Btn1,
        [Description("2")]
        Btn2,
        [Description("3")]
        Btn3,
        [Description("4")]
        Btn4,
        [Description("5")]
        Btn5,
        [Description("6")]
        Btn6,
        [Description("7")]
        Btn7,
        [Description("8")]
        Btn8,
        [Description("9")]
        Btn9,
        [Description("0")]
        Btn10,
        [Description("C")]
        Cancel,
        [Description("上")]
        Up,
        [Description("下")]
        Down,
        [Description("左")]
        Left,
        [Description("右")]
        Right,
        [Description("确认")]
        Enter,
        [Description("温度")]
        Temperature,
        [Description("背光")]
        Backlight,
        [Description("暗")]
        BacklightSmall,
        [Description("亮")]
        BacklightBig,
        [Description("日夜切换")]
        DayAndNight,
        [Description("声音小")]
        SoundSmall,
        [Description("声音大")]
        SoundBig,
        [Description("故障")]
        CurrentFault,
        [Description("信息")]
        Information,
        [Description("帮助")]
        Help
    }

    [DebuggerDisplay("Type = {Type} State = {State}")]
    public class ButtonInfo
    {
        public ButtonInfo(ButtonType type, int logicIndex)
        {
            LogicIndex = logicIndex;
            Type = type;
            State = MouseState.MouseUp;
        }

        public ButtonType Type { private set; get; }

        public MouseState State { set; get; }

        /// <summary>
        /// 逻辑的索引
        /// </summary>
        public int LogicIndex { private set; get; }
    }

}