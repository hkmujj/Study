using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnCTCS3ActionResponser : F8ReturnActionResponser
    {
        public F8ReturnCTCS3ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectCtcs(new SendModel<CTCSType>(CTCSType.CTCS3, SendModelType.Cancel));
        }
    }
}