using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkTrainDataActionResponser : F6OkActionResponser
    {
        public F6OkTrainDataActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            //NavigateOkWithGuide();

            ATP.SendInterface.SendTrainDataConfirm();
        }
    }
}