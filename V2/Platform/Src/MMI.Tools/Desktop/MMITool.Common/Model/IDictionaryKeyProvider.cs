using System;

namespace MMITool.Common.Model
{
    /// <summary>
    /// 字典key的提供者
    /// </summary>
    public interface IDictionaryKeyProvider
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        object GetKey();

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        int GetHashCode();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        bool Equals(object obj);
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IDictionaryKeyProvider<T> : IDictionaryKeyProvider where T : IEquatable<T> , IComparable
    {
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        new T GetKey();
    }
}