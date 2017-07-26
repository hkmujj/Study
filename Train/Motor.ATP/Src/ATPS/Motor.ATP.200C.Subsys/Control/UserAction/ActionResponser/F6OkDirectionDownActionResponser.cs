using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkDirectionDownActionResponser : F6OkActionResponser
    {
        public F6OkDirectionDownActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(TrainFreq.Down));
        }
    }
}