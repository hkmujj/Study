using System;
using System.Collections.ObjectModel;
using MMI.Facility.Interface.IndexDescription;

namespace MMI.Facility.Interface.Service
{
    /// <summary>
    /// 应用的索引解释器
    /// </summary>
    public interface IIndexInterpreter 
    {
        /// <summary>
        /// 
        /// </summary>
        ICommunicationDataKey Key { get; }


        /// <summary>
        /// 一个类型的意义值变化
        /// </summary>
        event Action<AppIndexType, ReadOnlyCollection<IIndexDescriptionProvider<int>>> IndexMeanChanged;

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<IIndexDescriptionProvider<int>> InBoolIndexDescriptionCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<IIndexDescriptionProvider<int>> OutBoolIndexDescriptionCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<IIndexDescriptionProvider<int>> InFloatIndexDescriptionCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<IIndexDescriptionProvider<int>> OutFloatIndexDescriptionCollection { get; }

        /// <summary>
        /// 
        /// </summary>
        ReadOnlyCollection<IIndexDescriptionProvider<int>> GetIndexDescriptionCollection(AppIndexType type);

        /// <summary>
        /// 注册索引的意义
        /// </summary>
        /// <param name="type"></param>
        /// <param name="index"></param>
        /// <param name="meaning"></param>
        void RegistIndexMeaning(AppIndexType type, int index, string meaning);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        void NotifyMeanChanged(AppIndexType type);
    }
}
