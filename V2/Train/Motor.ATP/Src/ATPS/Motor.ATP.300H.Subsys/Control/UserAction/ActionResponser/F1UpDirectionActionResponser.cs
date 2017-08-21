using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300H.Subsys.Control.UserAction.ActionResponser
{
    public class F1UpDirectionActionResponser : F1OrdinaryActionResponser
    {
        public F1UpDirectionActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputFreq>>()
                .Publish(new DriverInputEventArgs<DriverInputFreq>(new DriverInputFreq(TrainFreq.Up)));
        }

        public override void ResponseMouseUp()
        {
            //InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Root);
            base.ResponseMouseUp();

            InterfaceController.GotoRoot();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_载频确认取消上行));
        }
    }
}