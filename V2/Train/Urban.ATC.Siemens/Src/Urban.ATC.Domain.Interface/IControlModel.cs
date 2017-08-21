namespace Motor.ATP.Domain.Interface
{
    /// <summary>
    /// 控制模式
    /// </summary>
    public interface IControlModel : IATPPartial
    {
        ControlType CurrentControlType { get; }

        /// <summary>
        /// 准备进入的IControlModel
        /// </summary>
        ControlType NextControlType { get; }

        /// <summary>
        /// 已经生效
        /// </summary>
        bool InEffect { get; }
    }
}
