using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using Motor.ATP.Domain.Interface;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model
{
    public abstract class ATPPartialBase : NotificationObject, IATPPartial
    {
        private IInterfaceAdapterService m_IndexInterpreter;

        protected ATPPartialBase(ATPDomain parent)
        {
            Parent = parent;
        }

        public ATPDomain Parent { set; get; }

        IATP IATPPartial.Parent { get { return Parent; } }

        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get { return m_IndexInterpreter ?? (m_IndexInterpreter = App.Current.ServiceManager.GetService<IInterfaceAdapterService>()); }
        }
    }
}
