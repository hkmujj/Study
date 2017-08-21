using System;
using System.Diagnostics.Contracts;
using MMI.Facility.Interface.Data;
using Tram.CBTC.Infrasturcture.Events;

namespace Tram.CBTC.Infrasturcture.Extension
{
    using WarpperChangedFloat = CommunicationDataChangedWrapperArgs<float>;
    using WarpperChangedBool = CommunicationDataChangedWrapperArgs<bool>;

    public static class CommunicationDataChangedWrapperArgsExtension
    {

        public static void UpdateIfContains(this WarpperChangedBool data, string indexName,
            Action<bool> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.ChangedDatas.UpdateIfContains(data.InterfaceAdapterService.InBoolDictionary[indexName], updateAction);
        }

        public static void UpdateIfContains(this WarpperChangedBool data, string indexName,
            Action<string, int, bool> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.ChangedDatas.UpdateIfContains(data.InterfaceAdapterService.InBoolDictionary[indexName],
                (i, f) => updateAction(indexName, i, f));
        }

        public static void UpdateIfContains(this WarpperChangedFloat data, string indexName,
            Action<float> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.ChangedDatas.UpdateIfContains(data.InterfaceAdapterService.InFloatDictionary[indexName], updateAction);
        }


        public static void UpdateIfContains(this WarpperChangedFloat data, string indexName,
            Action<string, int, float> updateAction)
        {
            Contract.Requires(updateAction != null);
            data.ChangedDatas.UpdateIfContains(data.InterfaceAdapterService.InFloatDictionary[indexName],
                (i, f) => updateAction(indexName, i, f));
        }

        /// <summary>
        /// 如果有变化的值包含 index, 则更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="index"></param>
        /// <param name="updateAction"></param>
        public static void UpdateIfContains<T>(this CommunicationDataChangedWrapperArgs<T> datas, int index, Action<T> updateAction)
        {
           datas.ChangedDatas.UpdateIfContains(index, updateAction);
        }

        /// <summary>
        /// 如果有变化的值包含 index, 则更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="index"></param>
        /// <param name="updateAction"></param>
        public static void UpdateIfContains<T>(this CommunicationDataChangedWrapperArgs<T> datas, int index, Action<int, T> updateAction)
        {
            datas.ChangedDatas.UpdateIfContains(index, updateAction);
        }
    }
}