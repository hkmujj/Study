using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F2CTCS2ActionResponser : F2OrdinaryActionResponser
    {
        public F2CTCS2ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级确认取消CTCS2));
        }
    }
}