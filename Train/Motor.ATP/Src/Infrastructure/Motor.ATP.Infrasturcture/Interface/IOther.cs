using System;

namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOther : IATPPartial
    {
        /// <summary>
        /// 音量， 单位％
        /// </summary>
        float Volumne { set; get; }

        /// <summary>
        /// 亮度， 单位％
        /// </summary>
        float Light { set; get; }

        /// <summary>
        /// 当前显示时间和电脑时间差
        /// </summary>
        TimeSpan ShowingTimeDifference { set; get; }

        /// <summary>
        /// ATP当前时间
        /// </summary>
        DateTime NowInATP { set; get; }

        /// <summary>
        /// 当前显示的时间
        /// </summary>
        DateTime ShowingDateTime { get; }

        /// <summary>
        /// 时间标题
        /// </summary>
        bool DateTimeTitleVisible { get; }

        /// <summary>
        /// 当前日期是否可见
        /// </summary>
        bool DateVisible { get; }

        /// <summary>
        /// 当前时间是否可见
        /// </summary>
        bool TimeVisible { get; }
    }
}