using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    public class CancelActionResponser : DriverActionResponserBase
    {
        public CancelActionResponser(IDriverSelectableItem item, UserActionType userActionType = UserActionType.F8)
            : base(item, userActionType)
        {
        }

        public override void ResponseMouseDown()
        {

        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Root);
        }
    }
}