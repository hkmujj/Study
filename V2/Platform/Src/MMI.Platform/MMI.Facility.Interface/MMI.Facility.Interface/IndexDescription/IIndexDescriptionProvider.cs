using System;
using System.Diagnostics;

namespace MMI.Facility.Interface.IndexDescription
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    public interface IIndexDescriptionProvider<out TIndex> where TIndex : IEquatable<TIndex>
    {
        /// <summary>
        /// 
        /// </summary>
        TIndex Index { get; }

        /// <summary>
        /// 
        /// </summary>
        string Description { set; get; }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="TIndex"></typeparam>
    [DebuggerDisplay("Index={Index},  Description{Description}")]
    public class IndexDescriptionModel<TIndex> : IIndexDescriptionProvider<TIndex> where TIndex : IEquatable<TIndex>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="index"></param>
        /// <param name="description"></param>
        public IndexDescriptionModel(TIndex index, string description)
        {
            Index = index;
            Description = description;
        }

        public TIndex Index { get; private set; }

        public string Description { get; set; }
    }
}