using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Motor.ATP._300T.Subsys.Control.UserAction.ActionResponser
{
    public class F8ReturnEightTrucksActionResponser : F8ReturnActionResponser
    {
        private readonly List<string> m_TrainData;
        public F8ReturnEightTrucksActionResponser(IDriverSelectableItem item)
            : base(item)
        {
            m_TrainData = new List<string>() { "8" };
        }

        public override void ResponseMouseUp()
        {
            m_TrainData[0] = "16";

            ATP.SendInterface.SendTrainData(new SendModel<ReadOnlyCollection<string>>(m_TrainData.AsReadOnly(),SendModelType.Cancel));

            InterfaceController.GoBack();
        }
    }
}