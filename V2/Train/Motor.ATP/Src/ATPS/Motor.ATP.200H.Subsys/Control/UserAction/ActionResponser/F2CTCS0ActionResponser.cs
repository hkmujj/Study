using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F2CTCS0ActionResponser : F2OrdinaryActionResponser
    {
        public F2CTCS0ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }


        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级确认取消CTCS0));
        }
    }
}