using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B1ShuntingLinkActionResponser : B1OrdinaryActionResponser
    {
        public B1ShuntingLinkActionResponser(IDriverSelectableItem item) : base(item)
        {
        }

        /// <summary>
        /// 响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应
        /// </summary>
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKeys.Root_模式确认取消调车);
        }
    }
}