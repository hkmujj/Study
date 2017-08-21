using Microsoft.Practices.Prism.ViewModel;
using MMI.Facility.Interface;
using Motor.ATP.Infrasturcture.Interface;
using Motor.ATP.Infrasturcture.Interface.Service;

namespace Motor.ATP.Infrasturcture.Model
{
    public abstract class ATPPartialBase : NotificationObject, IATPPartial
    {
        private IInterfaceAdapterService m_IndexInterpreter;

        protected ATPPartialBase(ATPDomain parent)
        {
            Parent = parent;
        }

        public ATPDomain Parent { set; get; }

        protected IATP ATP { get { return Parent; } }

        IATP IATPPartial.Parent { get { return Parent; } }

        protected IInterfaceAdapterService InterfaceAdapterService
        {
            get { return m_IndexInterpreter ?? (m_IndexInterpreter = App.Current.ServiceManager.GetService<IInterfaceAdapterService>(Parent.ATPType.ToString())); }
        }
    }
}
