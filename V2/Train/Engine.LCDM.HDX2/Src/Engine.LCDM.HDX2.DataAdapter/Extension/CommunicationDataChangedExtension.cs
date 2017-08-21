using System;
using MMI.Facility.Interface.Data;

namespace Engine.LCDM.HDX2.DataAdapter.Extension
{
    public static class CommunicationDataChangedExtension
    {
        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<float> updateAction)
        {
            data.UpdateIfContains(HXD2Param.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[indexName],
                updateAction);
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<float> data, string indexName,
            Action<string, float> updateAction)
        {
            var index = HXD2Param.Instance.IndexDescriptionConfig.InFloatDescriptionDictionary[indexName];
            data.UpdateIfContains(index,
                ((i, f) => updateAction(indexName, f)));
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<bool> updateAction)
        {
            data.UpdateIfContains(HXD2Param.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[indexName],
                updateAction);
        }

        public static void UpdateIfContains(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action<string, bool> updateAction)
        {
            var index = HXD2Param.Instance.IndexDescriptionConfig.InBoolDescriptionDictionary[indexName];
            data.UpdateIfContains(index,
                ((i, f) => updateAction(indexName, f)));
        }
    }
}