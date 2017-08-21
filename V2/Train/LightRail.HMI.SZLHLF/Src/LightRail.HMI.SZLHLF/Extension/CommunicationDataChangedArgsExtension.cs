using System;
using System.Diagnostics.Contracts;
using System.Threading;
using LightRail.HMI.SZLHLF.Model;
using MMI.Facility.Interface.Data;

namespace LightRail.HMI.SZLHLF.Extension
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

        public static void UpdateIfContainsWhenTrue(this CommunicationDataChangedArgs<bool> data, string indexName,
            Action action)
        {
            Contract.Requires(action != null);
            var value = GlobalParam.Instance.InitParam.CommunicationDataService.ReadService.GetBoolAt(
                  GlobalParam.Instance.IndexDescription.InBoolDescriptionDictionary[indexName]);
            if (value)
            {
                action.Invoke();
            }
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

        /// <summary>
        /// 发送Bool值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="logic"></param>
        /// <param name="isClear">是否清楚发送数据 true清除 false 不清除</param>
        public static void SendBoolData(this string logic, bool value, bool isClear = false)
        {
            if (GlobalParam.Instance.IndexDescription == null)
            {
                return;
            }
            if (GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary.ContainsKey(logic))
            {
                GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(
                    GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[logic], value);
                if (isClear)
                {
                    ThreadPool.QueueUserWorkItem((satet) =>
                    {
                        Thread.Sleep(100);
                        GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeBool(
                   GlobalParam.Instance.IndexDescription.OutBoolDescriptionDictionary[logic], false);
                    });
                }
            }
        }

        /// <summary>
        /// 发送Float值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="logic"></param>
        public static void SendFloatData(this string logic, float value)
        {
            if (GlobalParam.Instance.IndexDescription == null)
            {
                return;
            }
            if (GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary.ContainsKey(logic))
            {
                GlobalParam.Instance.InitParam.CommunicationDataService.WriteService.ChangeFloat(
                    GlobalParam.Instance.IndexDescription.OutFloatDescriptionDictionary[logic], value);
            }
        }
    }
}