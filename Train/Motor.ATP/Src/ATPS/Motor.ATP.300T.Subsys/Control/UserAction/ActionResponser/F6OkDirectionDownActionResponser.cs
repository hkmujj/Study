using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP._300T.Subsys.Control.UserAction.StateProvider;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkDirectionDownActionResponser : F6OrdinaryActionResponser
    {
        public F6OkDirectionDownActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Down)));
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();
            
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_下行载频确认));
        }
    }
}