using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnCTCS0ActionResponser : F8ReturnActionResponser
    {
        public F8ReturnCTCS0ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectCtcs(new SendModel<CTCSType>(CTCSType.CTCS0, SendModelType.Cancel));
        }
    }
}