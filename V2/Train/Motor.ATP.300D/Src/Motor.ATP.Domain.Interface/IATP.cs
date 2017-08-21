using System.ComponentModel;

using Motor.ATP.Domain.Interface.Fault;

using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Interface
{
    public interface IATP : IIdentityProvide<ScreenIdentity>, IVisibility
    {
        ///// <summary>
        ///// ATP 对应的 RegionManager
        ///// </summary>
        //IRegionManager RegionManager { set; get; }

        /// <summary>
        /// ATP类型
        /// </summary>
        ATPType ATPType { get; }

        ///// <summary>
        ///// ATP 状态面板, 包括8个右边的按键
        ///// </summary>
        //IATPStateControlPanel StateControlPanel { get; }

        /// <summary>
        /// ATP 消息集合
        /// </summary>
        IMessage Message { get; }

        /// <summary>
        /// 车辆信息.
        /// </summary>
        ITrainInfo TrainInfo { get; }

        /// <summary>
        /// CTCS
        /// </summary>
        ICTCS CTCS { get; }

        /// <summary>
        /// 控制模式,
        /// </summary>
        IControlModel ControlModel { get; }

        /// <summary>
        /// 机车信号,
        /// </summary>
        ICabSignal CabSignal { get; }

        /// <summary>
        /// 报警介入
        /// </summary>
        IWarningIntervention WarningIntervention { get; }

        /// <summary>
        /// 速度监视
        /// </summary>
        ISpeedMonitoringSection SpeedMonitoringSection { get; }

        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="identity"></param>
        void Initalize(ScreenIdentity identity);

        /// <summary>
        /// 用户按键动作
        /// </summary>
        //void OnUserActed(UserActionEventArgs args);

        /// <summary>
        /// 故障发生
        /// </summary>
        void OnFaultOccused(IFaultItem faultItem);

        /// <summary>
        /// 故障解决
        /// </summary>
        void OnFaultFixed(IFaultItem faultItem);
    }
}
