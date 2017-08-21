using System;
using System.Collections.Generic;

namespace Subway.WuHanLine6.Extention
{
    /// <summary>
    ///
    /// </summary>
    public static class EnumerableExtention
    {
        /// <summary>
        ///
        /// </summary>
        /// <param name="value"></param>
        /// <param name="action"></param>
        /// <typeparam name="T"></typeparam>
        public static void ForEach<T>(this IEnumerable<T> value, Action<T> action)
        {
            foreach (var v in value)
            {
                if (action != null)
                {
                    action.Invoke(v);
                }
            }
        }
    }
}