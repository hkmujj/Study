using System.Collections.ObjectModel;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkEightTrucksActionResponser : F6OkActionResponser
    {
        public F6OkEightTrucksActionResponser(IDriverSelectableItem item)
            : base(item)
        {
        }

        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Publish(new DriverInputEventArgs<DriverInputTrainData>(new DriverInputTrainData(ATPType.ATP300T, new string[] { "8辆" })));
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_列车数据8辆确认));
        }
    }
}