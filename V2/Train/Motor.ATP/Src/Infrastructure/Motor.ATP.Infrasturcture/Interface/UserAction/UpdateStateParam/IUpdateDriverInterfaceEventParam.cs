using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Interface.UserAction.UpdateStateParam
{
    /// <summary>
    /// 更新状态接口的事件参数
    /// </summary>
    public interface IUpdateDriverInterfaceEventParam : IUpdateDriverInterfaceParam
    {
        /// <summary>
        /// 消息
        /// </summary>
        IInfomationItem InfomationItem { get; }

        /// <summary>
        /// 状态
        /// </summary>
        InformationState State { get; }
    }
}