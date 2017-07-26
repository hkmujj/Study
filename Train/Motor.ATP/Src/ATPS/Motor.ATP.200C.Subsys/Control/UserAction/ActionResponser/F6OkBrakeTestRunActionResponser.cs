using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.BrakeTest;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkBrakeTestRunActionResponser : F6OkActionResponser
    {
        public F6OkBrakeTestRunActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            ATP.SendInterface.EnsurceBrakeTestSelect(new SendModel<BrakeTestModel>(new BrakeTestModel<BrakeTestSelect>(BrakeTestSelect.Run)));
        }
    }
}