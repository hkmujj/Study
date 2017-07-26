using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6StartUpActionResponser: DriverActionResponserBase
    {
        public F6StartUpActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F6)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消启动));
        }
    }
}