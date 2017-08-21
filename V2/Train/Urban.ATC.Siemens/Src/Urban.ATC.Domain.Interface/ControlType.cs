using System.ComponentModel;

namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 控制模式， Description 为界面显示文本
    /// </summary>
    public enum ControlType
    {

        /// <summary>
        /// 无
        /// </summary>
        [Description("未知")]
        Unknown = 0,

        /// <summary>
        /// 完全监控
        /// </summary>
        [Description("完全")]
        FullSupervision = 1,

        /// <summary>
        /// 部分监控
        /// </summary>
        [Description("部分")]
        PartialSupervision = 2,

        /// <summary>
        /// 目视
        /// </summary>
        [Description("目视")]
        OnSight = 3,

        /// <summary>
        /// 引导
        /// </summary>
        [Description("引导")]
        CallingOn = 4,

        /// <summary>
        /// 调车
        /// </summary>
        [Description("调车")]
        Shunting = 5,

        /// <summary>
        /// LKJ
        /// </summary>
        [Description("LKJ")]
        LKJ = 6,
        ///// <summary>
        ///// 机车信号
        ///// </summary>
        //[Description("机信")]
        //CabSignal = 6,

        /// <summary>
        /// 待机
        /// </summary>
        [Description("待机")]
        StandBy = 7,

        /// <summary>
        /// 隔离
        /// </summary>
        [Description("隔离")]
        Isolated = 8,

        /// <summary>
        /// 反向行车
        /// </summary>
        [Description("反向")]
        RO,

        /// <summary>
        /// 冒进保护
        /// </summary>
        [Description("冒进")]
        Trip = 10,

        /// <summary>
        /// 冒进后
        /// </summary>
        [Description("冒后")]
        PostTrip = 11,

        /// <summary>
        /// 故障
        /// </summary>
        [Description("故障")]
        Fault = 12,

        /// <summary>
        /// 越行
        /// </summary>
        [Description("越行")]
        Overtaking = 13,

        /// <summary>
        /// 休眠
        /// </summary>
        [Description("休眠")]
        Sleep = 14,
    }
}