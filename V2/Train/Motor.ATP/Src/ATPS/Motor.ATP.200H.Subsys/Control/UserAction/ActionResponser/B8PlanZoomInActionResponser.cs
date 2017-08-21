using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Events;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class B8PlanZoomInActionResponser : B8OrdinaryActionResponser
    {
        public B8PlanZoomInActionResponser(IDriverSelectableItem item)
            : base(item)
        {

        }

        public override void ResponseMouseDown()
        {
            EventAggregator.GetEvent<ChangePlanScaleEvent>()
                .Publish(new ChangePlanScaleEvent.Args(ChangePlanScaleType.Increase, ATPType.ATP200H));
        }
    }
}