using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model.Extension
{
    public static class CommunicationDataWriteServiceExtension
    {
        private static IInterfaceAdapterService m_InterfaceAdapterService;

        private static IInterfaceAdapterService InterfaceAdapterService
        {
            get
            {
                return m_InterfaceAdapterService ??
                       (m_InterfaceAdapterService = App.Current.ServiceManager.GetService<IInterfaceAdapterService>());
            }
        }

        public static void ChangBool(this ICommunicationDataWriteService writeService, string indexName)
        {

        }
    }
}