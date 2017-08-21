using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnCabSignalActionResponser : F8ReturnActionResponser
    {
        public F8ReturnCabSignalActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();
            ATP.SendInterface.SelectControlMode(new SendModel<ControlType>(ControlType.LKJ, SendModelType.Cancel));
        }
    }
}