using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkShuntingActionResponser : F6OkActionResponser
    {
        public F6OkShuntingActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();
            ATP.SendInterface.SelectControlMode(new SendModel<ControlType>(ControlType.Shunting));
        }
    }
}