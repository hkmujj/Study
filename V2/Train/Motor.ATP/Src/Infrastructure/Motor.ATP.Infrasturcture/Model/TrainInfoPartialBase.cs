using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model
{
    public abstract class TrainInfoPartialBase : NotificationObject, ITrainInfoPartial
    {
        private IInterfaceAdapterService m_InterfaceAdapterService;

        protected TrainInfoPartialBase(ITrainInfo parent)
        {
            Parent = parent;
        }

        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get { return m_InterfaceAdapterService ?? (m_InterfaceAdapterService = App.Current.ServiceManager.GetService<IInterfaceAdapterService>(ATP.ATPType.ToString())); }
        }

        public ITrainInfo Parent { get; private set; }

        public IATP ATP { get { return Parent.Parent; } }
    }
}