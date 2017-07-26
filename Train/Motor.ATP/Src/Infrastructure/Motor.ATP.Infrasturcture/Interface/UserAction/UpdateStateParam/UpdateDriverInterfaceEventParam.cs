using System.Diagnostics;
using Motor.ATP.Infrasturcture.Interface.Infomation;

namespace Motor.ATP.Infrasturcture.Interface.UserAction.UpdateStateParam
{
    public class UpdateDriverInterfaceEventParam : UpdateDriverInterfaceParam, IUpdateDriverInterfaceEventParam
    {
        /// <summary>
        /// 消息
        /// </summary>
        public IInfomationItem InfomationItem { get; set; }

        /// <summary>
        /// 状态
        /// </summary>
        public InformationState State { get; set; }

        [DebuggerStepThrough]
        public UpdateDriverInterfaceEventParam(object sender) : base(sender)
        {
        }
    }
}