using Motor.ATP.Infrasturcture.Control.UserAction;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F8EnsureTrainDataActionResponser : ReturnActionResponser
    {
        public F8EnsureTrainDataActionResponser(IDriverSelectableItem item) : base(item)
        {

        }

        /// <summary>响应按键弹起，DriverActionResponserBase 提供发送输入事件，用于弹出框响应</summary>
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(InterfaceFactory.GetOrCreateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_列车数据)));
        }
    }
}