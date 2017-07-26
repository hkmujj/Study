using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP.Infrasturcture.Control.UserAction
{
    public class ReturnActionResponser : DriverActionResponserBase
    {
        public ReturnActionResponser(IDriverSelectableItem item, UserActionType userActionType = UserActionType.F8)
            : base(item, userActionType)
        {
        }

        public override void ResponseMouseDown()
        {
            
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.GoBack();
        }
    }
}