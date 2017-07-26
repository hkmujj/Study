using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkFreqActionResponser : F6OkActionResponser
    {
        public F6OkFreqActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.SendInterface.SendFreqConfirm();

            //NavigateOkWithGuide();
        }

        public override void ResponseMouseClick()
        {
            
        }
    }
}
