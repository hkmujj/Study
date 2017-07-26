using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F7RBCDataActionResponser : DriverActionResponserBase
    {
        public F7RBCDataActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F7)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_RBC数据));
        }
    }
}