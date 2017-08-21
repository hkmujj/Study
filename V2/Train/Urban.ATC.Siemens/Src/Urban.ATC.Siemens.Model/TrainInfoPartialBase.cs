using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model
{
    public abstract class TrainInfoPartialBase : NotificationObject, ITrainInfoPartial
    {
        private IInterfaceAdapterService m_InterfaceAdapterService;
        private ICommunicationDataService m_DataService;

        protected TrainInfoPartialBase(ITrainInfo parent)
        {
            Parent = parent;
        }

        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get { return m_InterfaceAdapterService ?? (m_InterfaceAdapterService = App.Current.ServiceManager.GetService<IInterfaceAdapterService>()); }
        }

        public ITrainInfo Parent { get; private set; }
    }
}