namespace Motor.ATP.Infrasturcture.Interface
{
    /// <summary>
    /// 控制模式
    /// </summary>
    public interface IControlModel : IATPPartial
    {
        /// <summary>
        /// 当前控制类型
        /// </summary>
        ControlType CurrentControlType { get; }

        /// <summary>
        /// 准备进入的IControlModel
        /// </summary>
        ControlType NextControlType { get; }

        /// <summary>
        /// 已经生效
        /// </summary>
        bool InEffect { get; }

        /// <summary>
        /// 可见性
        /// </summary>
        bool Visible { get; }

        /// <summary>
        /// 下一模式是否可见
        /// </summary>
        bool NextControlTypeVisible { get; }
    }

    
}
