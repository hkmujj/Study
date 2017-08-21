using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F8CancelCheckDriverIDActionResponser : DriverActionResponserBase
    {
        public F8CancelCheckDriverIDActionResponser(IDriverSelectableItem item) : base(item, UserActionType.F8)
        {
        }

        /// <summary>
        /// 响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_司机号));
        }
    }
}