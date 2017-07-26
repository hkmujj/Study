using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Events;
using Motor.ATP.Infrasturcture.Interface.UserAction;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class B9PlanZoomOutActionResponser : B9OrdinaryActionResponser
    {
        public B9PlanZoomOutActionResponser(IDriverSelectableItem item)
            : base(item)
        {

        }

        public override void ResponseMouseDown()
        {
            EventAggregator.GetEvent<ChangePlanScaleEvent>()
                .Publish(new ChangePlanScaleEvent.Args(ChangePlanScaleType.Reduce, ATPType.ATP300S));
        }
    }
}