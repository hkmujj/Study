using MMI.Facility.Interface.Service;
using Urban.GuiYang.DDU.Model;

namespace Urban.GuiYang.DDU.Extension
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

        public static void ChangeInBoolOf(this IWritableCommunicationDataReadService readService, string name, bool value, bool notifyDataChanged= false)
        {
            readService.ChangeBool(GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[name], value, notifyDataChanged);
        }

        public static void ChangeOutBoolOf(this ICommunicationDataService dataService, string name, bool value)
        {
            dataService.WriteService.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[name], value);
        }

        public static void ChangeOutFloatOf(this ICommunicationDataService dataService, string name, float value)
        {
            dataService.WriteService.ChangeFloat(
                GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[name], value);
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