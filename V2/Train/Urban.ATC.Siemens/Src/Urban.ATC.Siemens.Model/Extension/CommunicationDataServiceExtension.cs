using MMI.Facility.Interface;
using MMI.Facility.Interface.Service;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model.Extension
{
    public static class CommunicationDataServiceExtension
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

        public static bool GetInBoolOf(this ICommunicationDataService dataService, string name)
        {
            return GetInBoolOf(dataService.ReadService, name);
        }

        public static bool GetInBoolOf(this ICommunicationDataReadService readService, string name)
        {
            return readService.ReadOnlyBoolDictionary[InterfaceAdapterService.InBoolDictionary[name]];
        }


        public static bool GetInBoolOldOf(this ICommunicationDataService dataService, string name)
        {
            return GetInBoolOldOf(dataService.ReadService, name);
        }

        public static bool GetInBoolOldOf(this ICommunicationDataReadService readService, string name)
        {
            return readService.ReadOnlyBoolOldDictionary[InterfaceAdapterService.InBoolDictionary[name]];
        }

        public static float GetInFloatOf(this ICommunicationDataService dataService, string name)
        {
            return GetInFloatOf(dataService.ReadService, name);
        }

        public static float GetInFloatOf(this ICommunicationDataReadService readService, string name)
        {
            return readService.ReadOnlyFloatDictionary[InterfaceAdapterService.InFloatDictionary[name]];
        }


        public static float GetInFloatOldOf(this ICommunicationDataService dataService, string name)
        {
            return GetInFloatOldOf(dataService.ReadService, name);
        }

        public static float GetInFloatOldOf(this ICommunicationDataReadService readService, string name)
        {
            return readService.ReadOnlyFloatOldDictionary[InterfaceAdapterService.InFloatDictionary[name]];
        }
    }
}