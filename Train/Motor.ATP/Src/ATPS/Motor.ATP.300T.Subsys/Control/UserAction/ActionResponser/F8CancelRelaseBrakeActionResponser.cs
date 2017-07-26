using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8CancelRelaseBrakeActionResponser : F8CancelActionResponser
    {
        public F8CancelRelaseBrakeActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SendRelease(new SendModel<object>(null, SendModelType.Cancel));
        }
    }
}