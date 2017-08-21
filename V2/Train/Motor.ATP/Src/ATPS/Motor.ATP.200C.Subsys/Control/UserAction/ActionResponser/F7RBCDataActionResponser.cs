using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.ServiceLocation;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Events;
using Motor.ATP.Infrasturcture.Interface.UserAction;
using Motor.ATP.Infrasturcture.Model.Resources;
using Motor.ATP.Infrasturcture.Model.UserAction;

namespace Motor.ATP._200C.Subsys.Control.UserAction.ActionResponser
{
    public class F7RBCDataActionResponser : DriverActionResponserBase
    {
        private DataInputtedFromEvent m_DataInputtedFromEvent;

        public F7RBCDataActionResponser(IDriverSelectableItem item)
            : base(item, UserActionType.F7)
        {
            m_DataInputtedFromEvent =
                ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<DataInputtedFromEvent>();
        }

        public override void ResponseMouseUp()
        {
            m_DataInputtedFromEvent.Publish(new DataInputtedFromEvent.Args(ATP.ATPType, typeof (RBCDataModel),
                DataInputtedFrom.UserActive));
            InterfaceController.UpdateDriverInterface(DriverInterfaceKey.Parser(DriverInterfaceKeys.Root_数据_RBC数据));
        }
    }
}