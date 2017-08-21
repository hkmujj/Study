using System;
using System.Diagnostics;
using System.Drawing;

namespace Motor.HMI.CRH1A.Alarm.Fault
{
    [AttributeUsage(AttributeTargets.Field, AllowMultiple = false, Inherited = false)]
    [DebuggerDisplay("FaultShowStyle = {FaultShowStyle} Color = {Color}")]
    sealed class FaultShowAttribute : Attribute
    {

        public FaultShowAttribute()
            : this("Yellow", FaultShowStyle.Unkown)
        {

        }

        public FaultShowAttribute(FaultShowStyle showStyle)
            : this("Yellow", showStyle)
        {

        }

        public FaultShowAttribute(string color)
            : this(color, FaultShowStyle.Unkown)
        {

        }



        public FaultShowAttribute(string color, FaultShowStyle showStyle)
        {
            Color = Color.FromName(color);
            ShowStyle = showStyle;
        }
        public FaultShowAttribute(FaultShowStyle showStyle, string color)
        {
            Color = Color.FromName(color);
            ShowStyle = showStyle;
        }


        public Color Color { private set; get; }

        public FaultShowStyle ShowStyle { private set; get; }
    }

    enum FaultShowStyle
    {
        Unkown = 0,

        /// <summary>
        /// 弹框
        /// </summary>
        PopView,

        /// <summary>
        /// 页脚显示
        /// </summary>
        ShowFeet,

        /// <summary>
        /// 弹框并页脚显示
        /// </summary>
        PopViewAndShowFeet,

        /// <summary>
        /// 全屏
        /// </summary>
        FullScreen
    }
}