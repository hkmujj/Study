using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F1DataActionResponser : DriverActionResponserBase
    {
        public F1DataActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F1)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据));
        }
    }
}