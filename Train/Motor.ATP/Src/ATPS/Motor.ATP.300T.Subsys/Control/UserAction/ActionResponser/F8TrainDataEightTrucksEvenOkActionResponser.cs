using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Control.UserAction.ActionResponser;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Infomation;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8TrainDataEightTrucksEvenOkActionResponser : F8OrdinaryActionResponser
    {
        private readonly List<string> m_TrainData;
        public F8TrainDataEightTrucksEvenOkActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            m_TrainData = new List<string>() { "8" };
        }

        public override void ResponseMouseUp()
        {
            InterfaceController.GotoRoot();

            m_TrainData[0] = "8";

            ATP.SendInterface.SendTrainData(new SendModel<ReadOnlyCollection<string>>(m_TrainData.AsReadOnly()));

        }

        public override void ResponseMouseClick()
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Publish(new DriverInputEventArgs<DriverInputTrainData>(new DriverInputTrainData(ATPType.ATP300T, new string[] { "" })));
        }
    }
}
