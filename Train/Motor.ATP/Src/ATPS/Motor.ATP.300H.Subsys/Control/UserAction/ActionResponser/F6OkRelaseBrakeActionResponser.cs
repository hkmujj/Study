using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkRelaseBrakeActionResponser : F6OkActionResponser
    {
        public F6OkRelaseBrakeActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SendRelease(new SendModel<object>());
        }
    }
}