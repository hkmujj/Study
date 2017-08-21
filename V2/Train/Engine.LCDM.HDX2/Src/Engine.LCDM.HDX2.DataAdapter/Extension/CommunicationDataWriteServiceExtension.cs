using MMI.Facility.Interface.Service;

namespace Engine.LCDM.HDX2.DataAdapter.Extension
{
    public static class CommunicationDataWriteServiceExtension
    {
        public static void ChangeBool(this ICommunicationDataWriteService writeService, string indexName, bool value)
        {
            writeService.ChangeBool(HXD2Param.Instance.IndexDescriptionConfig.OutBoolDescriptionDictionary[indexName],
                value);
        }

        public static void ChangeFloat(this ICommunicationDataWriteService writeService, string indexName, float value)
        {
            writeService.ChangeFloat(HXD2Param.Instance.IndexDescriptionConfig.OutFloatDescriptionDictionary[indexName],
                value);
        }
    }
}