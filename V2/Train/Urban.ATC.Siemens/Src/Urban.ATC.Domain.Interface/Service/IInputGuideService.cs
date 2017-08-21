using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface.InputGuide;
using Motor.ATP.Domain.Interface.UserAction;

namespace Motor.ATP.Domain.Interface.Service
{
    public interface IInputGuideService : IService, IATPPartial
    {

        /// <summary>
        /// 当前输入节点
        /// </summary>
        IInputNode CurrentInputNode { get; }

        /// <summary>
        /// 设置当前输入步骤
        /// </summary>
        /// <param name="stepType"></param>
        void SelectInputStep(InputStepType stepType);

        /// <summary>
        /// 下一步
        /// </summary>
        bool Goforward();

        /// <summary>
        /// 导航链路中是否有 key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool Contains(DriverInterfaceKey key);

    }

}