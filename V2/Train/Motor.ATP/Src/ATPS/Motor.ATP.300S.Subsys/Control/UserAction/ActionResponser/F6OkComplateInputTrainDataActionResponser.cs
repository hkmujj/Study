using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;

namespace Motor.ATP._300S.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkComplateInputTrainDataActionResponser : F6OkActionResponser
    {
        private readonly List<string> m_InputtedTrainData;

        public F6OkComplateInputTrainDataActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            m_InputtedTrainData = new List<string>();
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Subscribe(RecvInputTrainData, ThreadOption.PublisherThread, false,
                    args => args.SelectedContent.ATPType == ATPType.ATP300S);
        }

        private void RecvInputTrainData(DriverInputEventArgs<DriverInputTrainData> obj)
        {
            m_InputtedTrainData.Clear();
            m_InputtedTrainData.AddRange(obj.SelectedContent.InputtedTrainData);
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.SendInterface.SendTrainData(new SendModel<ReadOnlyCollection<string>>(m_InputtedTrainData.AsReadOnly()));
        }
    }
}