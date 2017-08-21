using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F2TelNumberActionResponser : DriverActionResponserBase
    {
        public F2TelNumberActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F2)
        {
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKeys.Root_ ‰»ÎµÁª∞∫≈¬Î);
        }
    }
}