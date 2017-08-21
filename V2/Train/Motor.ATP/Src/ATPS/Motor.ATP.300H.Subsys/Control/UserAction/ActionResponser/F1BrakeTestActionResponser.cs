using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F1BrakeTestActionResponser : F1OrdinaryActionResponser
    {
        public F1BrakeTestActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        /// <summary>
        /// 响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_制动测试选择));
        }
    }
}