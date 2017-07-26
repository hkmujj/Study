using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F3NetNumberActionResponser : DriverActionResponserBase
    {
        public F3NetNumberActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F3)
        {
        }

        public override void ResponseMouseUp()
        {
            //InterfaceController.UpdateDriverInterface(DriverInterfaceKeys.Root_ ‰»ÎRBCID);
        }
    }
}