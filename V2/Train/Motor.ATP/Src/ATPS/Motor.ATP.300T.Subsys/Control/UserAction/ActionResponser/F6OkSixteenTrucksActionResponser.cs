using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkSixteenTrucksActionResponser : F6OkActionResponser
    {
        public F6OkSixteenTrucksActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Publish(new DriverInputEventArgs<DriverInputTrainData>(new DriverInputTrainData(ATPType.ATP300T, new[] { "16辆" })));
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_列车数据16辆确认));
        }
    }
}