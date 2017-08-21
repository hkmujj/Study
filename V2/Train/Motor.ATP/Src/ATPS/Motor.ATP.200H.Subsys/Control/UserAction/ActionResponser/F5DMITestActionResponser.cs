using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F5DMITestActionResponser : F5OrdinaryActionResponser
    {
        public F5DMITestActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            ATP.SendInterface.SendDMITest(new SendModel<object>());

            ATP.UpdateDriverInterface(DriverInterfaceKeys.Root_∆‰À¸_DMI≤‚ ‘);
        }
    }
}