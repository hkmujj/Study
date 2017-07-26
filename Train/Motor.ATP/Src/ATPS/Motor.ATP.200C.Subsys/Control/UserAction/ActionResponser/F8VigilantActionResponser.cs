using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.UserAction;
using Motor.ATP._200C.Subsys.Events;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F8VigilantActionResponser : DriverActionResponserBase
    {
        public F8VigilantActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F8)
        {
        }


        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<EnsureMessageEvent>()
                .Publish(new EnsureMessageEvent.Args(ATP.ATPType, InfomationResponseType.Vigilant));

            ATP.SendInterface.SendAlert();
        }
    }
}