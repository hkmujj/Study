using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6RequestOkTrainIDActionResponser : F6OrdinaryActionResponser
    {
        public F6RequestOkTrainIDActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        /// <summary>响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应</summary>
        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_数据确认取消车次号);
        }
    }
}