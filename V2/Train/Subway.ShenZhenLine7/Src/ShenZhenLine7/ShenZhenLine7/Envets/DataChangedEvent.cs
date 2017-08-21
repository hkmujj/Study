using System.Collections.Generic;
using Microsoft.Practices.Prism.Events;

namespace Subway.ShenZhenLine7.Envets
{
    /// <summary>
    /// 
    /// </summary>
    public class BoolDataChangedEvent : CompositePresentationEvent<DataChangedEventArgs<bool>>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    public class FloatDataChangedEvent : CompositePresentationEvent<DataChangedEventArgs<float>>
    {

    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DataChangedEventArgs<T>
    {
        /// <summary>
        /// 
        /// </summary>
        public IDictionary<int, T> Data { get; set; }

    }
}