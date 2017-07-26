using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F7RelievingBrakeActionResponser : DriverActionResponserBase
    {
        public F7RelievingBrakeActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F7)
        {
        }

        public override void ResponseMouseClick()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消缓解制动));
        }
    }
}