using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Practices.Prism.Events;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Extension;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Events;
using Motor.ATP.Infrasturcture.Model.Events.DriverInputEvents;
using Motor.ATP.Infrasturcture.Model.Resources;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F6OkComplateInputTrainDataActionResponser : F6OkActionResponser
    {
        private List<string> m_InputtedTrainData;

        public F6OkComplateInputTrainDataActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            EventAggregator.GetEvent<DriverInputEvent<DriverInputTrainData>>()
                .Subscribe(RecvInputTrainData, ThreadOption.PublisherThread, false,
                    args => args.SelectedContent.ATPType == ATPType.ATP200C);
        }

        private void RecvInputTrainData(DriverInputEventArgs<DriverInputTrainData> obj)
        {
            m_InputtedTrainData = obj.SelectedContent.InputtedTrainData.ToList();
        }

        public override void ResponseMouseUp()
        {
            base.ResponseMouseUp();

            ATP.SendInterface.SendTrainData(new SendModel<ReadOnlyCollection<string>>(m_InputtedTrainData.AsReadOnly()));

            ATP.GetInterfaceController()
                .UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_确认取消列车数据));
        }
    }
}