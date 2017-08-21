using System;
using System.Diagnostics.Contracts;
using Engine.TAX2.SS7C.Model;
using MMI.Facility.Interface.Data;

namespace Engine.TAX2.SS7C.Extension
{
    public static class CommunicationDataChangedArgsExtension
    {

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<bool> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[indexName],
                updateAction);
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<string, int, bool> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[indexName],
                (i, f) => updateAction(indexName, i, f));
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<float> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(GlobalParam.Instance.IndexDescription.InFloatDescriptionDictionary[indexName],
                updateAction);
        }


        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<string, int, float> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.UpdateIfContains(GlobalParam.Instance.IndexDescription.InFloatDescriptionDictionary[indexName],
                (i, f) => updateAction(indexName, i, f));
        }
    }
}