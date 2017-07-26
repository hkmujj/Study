using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F2DownDirectionActionResponser : F2OrdinaryActionResponser
    {
        public F2DownDirectionActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_载频确认取消下行));
        }
    }
}