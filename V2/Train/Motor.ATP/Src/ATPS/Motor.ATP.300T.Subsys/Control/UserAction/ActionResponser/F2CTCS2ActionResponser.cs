using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F2CTCS2ActionResponser : F1OrdinaryActionResponser
    {
        public F2CTCS2ActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectCtcs(new SendModel<CTCSType>(CTCSType.CTCS2));
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.GotoRoot();
        }
    }
}