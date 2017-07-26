using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F3SetLightActionResponser : F3OrdinaryActionResponser
    {
        public F3SetLightActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }
        public override void ResponseMouseUp()
        {
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_其它_设置亮度));
        }
    }
}