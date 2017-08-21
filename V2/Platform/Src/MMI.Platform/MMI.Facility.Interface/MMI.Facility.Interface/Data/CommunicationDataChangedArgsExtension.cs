using System;
using System.Diagnostics.Contracts;

namespace MMI.Facility.Interface.Data
{
    /// <summary>
    /// 
    /// </summary>
    public static class CommunicationDataChangedArgsExtension
    {
        /// <summary>
        /// 如果有变化的值包含 index, 则更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="index"></param>
        /// <param name="updateAction"></param>
        public static void UpdateIfContains<T>(this CommunicationDataChangedArgs<T> datas, int index, Action<T> updateAction)
        {
            Contract.Requires(updateAction != null);
            Contract.Requires(index != int.MaxValue);

            if (datas.NewValue.ContainsKey(index))
            {
                updateAction(datas.NewValue[index]);
            }
        }

        /// <summary>
        /// 如果有变化的值包含 index, 则更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="datas"></param>
        /// <param name="index"></param>
        /// <param name="updateAction"></param>
        public static void UpdateIfContains<T>(this CommunicationDataChangedArgs<T> datas, int index, Action<int, T> updateAction)
        {
            Contract.Requires(updateAction != null);
            Contract.Requires(index != int.MaxValue);

            if (datas.NewValue.ContainsKey(index))
            {
                updateAction(index, datas.NewValue[index]);
            }
        }
    }
}