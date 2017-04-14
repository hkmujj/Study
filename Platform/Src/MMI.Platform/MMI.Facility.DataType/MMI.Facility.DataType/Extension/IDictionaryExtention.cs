using System.Collections.Generic;

namespace MMI.Facility.DataType.Extension
{
    public static class DictionaryExtention
    {
        public static ReadOnlyDictionary<TKey, TValue> AsReadOnlyDictionary<TKey, TValue>(this IDictionary<TKey, TValue> sourceDic)
        {
            return new ReadOnlyDictionary<TKey, TValue>(sourceDic);
        }
    }
}
