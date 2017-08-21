namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 控制模式
    /// </summary>
    public interface IControlModel : IATPPartial
    {
        ControlType Type { get; }

        /// <summary>
        /// 准备进入的IControlModel
        /// </summary>
        IControlModel NextControlModel { get; }

        /// <summary>
        /// 已经生效
        /// </summary>
        bool InEffect { get; }
    }

    public enum ControlType
    {

        /// <summary>
        /// 无
        /// </summary>
        Unknown,

        /// <summary>
        /// 完全监控
        /// </summary>
        FullSupervision,

        /// <summary>
        /// 部分监控
        /// </summary>
        PartialSupervision,

        /// <summary>
        /// 引导
        /// </summary>
        CallingOn,

        /// <summary>
        /// 调车
        /// </summary>
        Shunting,

        /// <summary>
        /// 目视
        /// </summary>
        OnSight,

        /// <summary>
        /// 待机
        /// </summary>
        StandBy,

        /// <summary>
        /// LKJ
        /// </summary>
        LKJ,

        /// <summary>
        /// 冒进保护
        /// </summary>
        Trip,

        /// <summary>
        /// 冒进后
        /// </summary>
        PostTrip,

        /// <summary>
        /// 越行
        /// </summary>
        Overtaking
    }

}
