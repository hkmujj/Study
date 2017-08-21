using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnOnSightActionResponser : F8ReturnActionResponser
    {
        public F8ReturnOnSightActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();
            ATP.SendInterface.SelectControlMode(new SendModel<ControlType>(ControlType.OnSight, SendModelType.Cancel));
        }
    }
}