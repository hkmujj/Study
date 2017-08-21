using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200H.Subsys.Control.UserAction.ActionResponser
{
    public class B5ShuntingLinkActionResponser : B5OrdinaryActionResponser
    {
        public B5ShuntingLinkActionResponser(IDriverSelectableItem item) : base(item)
        {

        }

        public override void ResponseMouseUp()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Up), ATP));

            InterfaceController.GotoRoot();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消载频));
           // InterfaceController.UpdateDriverInterface(DriverInterfaceKeys.Root_确认取消载频);
        }
    }
    public class B6ShuntingLinkActionResponser : B6OrdinaryActionResponser
    {
        public B6ShuntingLinkActionResponser(IDriverSelectableItem item) : base(item)
        {

        }

        public override void ResponseMouseUp()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Down), ATP));

            InterfaceController.GotoRoot();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消载频));
        }
    }
}