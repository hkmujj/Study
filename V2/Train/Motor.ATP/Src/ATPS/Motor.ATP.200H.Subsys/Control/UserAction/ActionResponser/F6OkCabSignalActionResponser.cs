using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkCabSignalActionResponser : F6OkActionResponser
    {
        public F6OkCabSignalActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            ATP.SendInterface.SelectControlMode(new SendModel<ControlType>(ControlType.LKJ));
        }
    }
}