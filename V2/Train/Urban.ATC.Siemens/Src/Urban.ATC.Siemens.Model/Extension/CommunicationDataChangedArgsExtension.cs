using System;
using System.Diagnostics.Contracts;
using MMI.Facility.Interface;
using MMI.Facility.Interface.Data;
using Motor.ATP.Domain.Interface.Service;

namespace Motor.ATP.Domain.Model.Extension
{
    public static class CommunicationDataChangedArgsExtension
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

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<bool> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(InterfaceAdapterService.InBoolDictionary[indexName], updateAction);
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<string, int, bool> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(InterfaceAdapterService.InBoolDictionary[indexName],
                (i, f) => updateAction(indexName, i, f));
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<float> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(InterfaceAdapterService.InFloatDictionary[indexName], updateAction);
        }


        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<string, int, float> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(InterfaceAdapterService.InFloatDictionary[indexName],
                (i, f) => updateAction(indexName, i, f));
        }
    }
}