using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8OkCTCS3ActionResponser : F8OrdinaryActionResponser
    {
        public F8OkCTCS3ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectCtcs(new SendModel<CTCSType>(CTCSType.CTCS3));
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.GotoRoot();
        }
    }
}