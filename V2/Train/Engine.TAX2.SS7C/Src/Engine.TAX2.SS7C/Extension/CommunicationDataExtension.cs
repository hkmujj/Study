using System;
using System.Diagnostics.Contracts;
using Engine.TAX2.SS7C.Model;
using MMI.Facility.Interface.Data;
using MMI.Facility.Interface.Service;

namespace Engine.TAX2.SS7C.Extension
{
    public static class WritableCommunicationDataExtension
    {
        public static void ChangeBool(this IWritableCommunicationDataReadService writable, string name, bool value)
        {
            writable.ChangeBool(GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[name], value);
        }

        public static void ChangeFloat(this IWritableCommunicationDataReadService writable, string name, float value)
        {
            writable.ChangeFloat(GlobalParam.Instance.IndexDescription.InFloatDescriptionDictionary[name], value);
        }

        public static void ChangeBool(this ICommunicationDataWriteService writable, string name, bool value)
        {
            writable.ChangeBool(GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[name], value);
        }

        public static void ChangeFloat(this ICommunicationDataWriteService writable, string name, float value)
        {
            writable.ChangeFloat(GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[name], value);
        }
    }

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