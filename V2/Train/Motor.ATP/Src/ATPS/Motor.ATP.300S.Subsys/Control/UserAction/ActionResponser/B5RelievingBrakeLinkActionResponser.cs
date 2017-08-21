using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B5RelievingBrakeLinkActionResponser : B5OrdinaryActionResponser
    {
        public B5RelievingBrakeLinkActionResponser(IDriverSelectableItem item) : base(item)
        {
        }


        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消缓解制动));
        }
    }
}