using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F1RBCIDActionResponser : DriverActionResponserBase
    {
        public F1RBCIDActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F1)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKeys.Root_ ‰»ÎRBCID);
        }
    }
}