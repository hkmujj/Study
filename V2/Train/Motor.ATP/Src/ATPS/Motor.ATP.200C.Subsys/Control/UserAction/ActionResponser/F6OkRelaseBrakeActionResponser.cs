using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP._200C.Subsys.Events;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkRelaseBrakeActionResponser : F6OkActionResponser
    {
        public F6OkRelaseBrakeActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            base.ResponseMouseClick();

            EventAggregator.GetEvent<EnsureMessageEvent>()
                .Publish(new EnsureMessageEvent.Args(ATP.ATPType, InfomationResponseType.Relase));

            ATP.SendInterface.SendRelease(new SendModel<object>());
        }
    }
}