using Motor.HMI.CRH380BG.Model;
using MMI.Facility.Interface.Service;

namespace Motor.HMI.CRH380BG.Extension
{
    public static class CommunicationDataServiceExtension
    {
        public static bool GetInBoolOf(this ICommunicationDataService dataService, string name)
        {
            return GetInBoolOf(dataService.ReadService, name);
        }

        public static bool GetInBoolOf(this ICommunicationDataReadService readService, string name)
        {
            return
                readService.ReadOnlyBoolDictionary[
                    GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[name]];
        }

        public static void ChangedInBoolOf(this IWritableCommunicationDataReadService readService, string name, bool value)
        {
            readService.ChangeBool(GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[name], value);
        }


        public static bool GetInBoolOldOf(this ICommunicationDataService dataService, string name)
        {
            return GetInBoolOldOf(dataService.ReadService, name);
        }

        public static bool GetInBoolOldOf(this ICommunicationDataReadService readService, string name)
        {
            return
                readService.ReadOnlyBoolOldDictionary[
                    GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[name]];
        }

        public static float GetInFloatOf(this ICommunicationDataService dataService, string name)
        {
            return GetInFloatOf(dataService.ReadService, name);
        }

        public static float GetInFloatOf(this ICommunicationDataReadService readService, string name)
        {
            return
                readService.ReadOnlyFloatDictionary[
                    GlobalParam.Instance.IndexDescription.InFloatDescriptionDictionary[name]];
        }


        public static float GetInFloatOldOf(this ICommunicationDataService dataService, string name)
        {
            return GetInFloatOldOf(dataService.ReadService, name);
        }

        public static float GetInFloatOldOf(this ICommunicationDataReadService readService, string name)
        {
            return
                readService.ReadOnlyFloatOldDictionary[
                    GlobalParam.Instance.IndexDescription.InFloatDescriptionDictionary[name]];
        }
    }
}