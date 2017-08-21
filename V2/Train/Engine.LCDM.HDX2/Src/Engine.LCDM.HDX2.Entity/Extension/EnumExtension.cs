using System;
using System.Collections.Generic;
using System.Linq;

namespace Engine.LCDM.HDX2.Entity.Extension
{
    public static class EnumExtension
    {
        public static Dictionary<Type, Enum[]> m_BufferDictionary = new Dictionary<Type, Enum[]>();

        public static Enum GetNextEnum(this Enum obj)
        {
            var type = obj.GetType();
            if (!m_BufferDictionary.ContainsKey(type))
            {
                m_BufferDictionary.Add(type, Enum.GetValues(type).Cast<Enum>().ToArray());
            }

            var arr = (Enum[])m_BufferDictionary[type];
            var idx = Array.IndexOf(arr, obj);
            return arr[(idx + 1)%arr.Length];
            
        }
    }
}