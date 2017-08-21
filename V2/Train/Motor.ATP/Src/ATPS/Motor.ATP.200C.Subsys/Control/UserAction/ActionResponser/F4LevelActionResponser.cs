using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F4LevelActionResponser : F4OrdinaryActionResponser
    {
        public F4LevelActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_等级));
        }
    }
}