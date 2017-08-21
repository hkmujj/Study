using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnCTCS2ActionResponser : F8ReturnActionResponser
    {
        public F8ReturnCTCS2ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectCtcs(new SendModel<CTCSType>(CTCSType.CTCS2, SendModelType.Cancel));
        }
    }
}