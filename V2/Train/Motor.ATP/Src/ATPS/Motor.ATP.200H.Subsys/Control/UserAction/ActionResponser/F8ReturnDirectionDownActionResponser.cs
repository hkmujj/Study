using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnDirectionDownActionResponser : F8ReturnActionResponser
    {
        public F8ReturnDirectionDownActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(TrainFreq.Down, SendModelType.Cancel));
        }
    }
}