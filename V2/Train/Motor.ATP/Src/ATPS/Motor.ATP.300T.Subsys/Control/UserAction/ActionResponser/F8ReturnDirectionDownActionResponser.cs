using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnDirectionDownActionResponser : F8ReturnActionResponser
    {
        public F8ReturnDirectionDownActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();
            ATP.SendInterface.SelectFreq(new SendModel<TrainFreq>(TrainFreq.Down, SendModelType.Cancel));
        }
    }
}