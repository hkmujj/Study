using System;
using System.Collections.Generic;
using System.Drawing;

namespace MMITool.Common.Util
{
    /// <summary>
    /// FuncDictionary
    /// </summary>
    public class DynamicInvokeHelper
    {
        /// <summary>
        /// Dictionary
        /// </summary>
        public static IDictionary<Type, Delegate> Dictionary
        {
            get;
            private set;
        }

        static DynamicInvokeHelper()
        {
            if (Dictionary == null)
            {
                Dictionary = CreateDictionary();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type"></param>
        /// <param name="arg"></param>
        /// <returns></returns>
        public static object DynamicInvoke(Type type, string arg)
        {
            if (type == null)
            {
                return null;
            }

            if (!Dictionary.ContainsKey(type))
            {
                return null;
            }

            Delegate action = Dictionary[type];

            return action.DynamicInvoke(new object[] { arg });
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arg"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T DynamicInvoke<T>(string arg) 
        {
            return (T)DynamicInvoke(typeof (T), arg) ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static IDictionary<Type, Delegate> CreateDictionary()
        {
            var dictionary = new Dictionary<Type, Delegate>();

            dictionary.Add(typeof(string), new Func<string, string>(p => p));
            dictionary.Add(typeof(decimal), new Func<string, decimal>(p => p.AsDecimal()));
            dictionary.Add(typeof(DateTime), new Func<string, DateTime>(p => p.AsDateTime()));
            dictionary.Add(typeof(float), new Func<string, float>(p => p.AsFloat()));
            dictionary.Add(typeof(double), new Func<string, double>(p => p.AsDouble()));
            dictionary.Add(typeof(int), new Func<string, int>(p => p.AsInt()));
            dictionary.Add(typeof(byte), new Func<string, byte>(p => p.AsByte()));
            dictionary.Add(typeof(sbyte), new Func<string, sbyte>(p => p.AsSbyte()));
            dictionary.Add(typeof(short), new Func<string, short>(p => p.AsShort()));
            dictionary.Add(typeof(ushort), new Func<string, ushort>(p => p.AsUshort()));
            dictionary.Add(typeof(uint), new Func<string, uint>(p => p.AsUint()));
            dictionary.Add(typeof(long), new Func<string, long>(p => p.AsLong()));
            dictionary.Add(typeof(ulong), new Func<string, ulong>(p => p.AsUlong()));
            dictionary.Add(typeof(char), new Func<string, char>(p => p.AsChar()));
            dictionary.Add(typeof(bool), new Func<string, bool>(p => p.AsBool()));
            dictionary.Add(typeof(Color), new Func<string, Color>(p => p.AsColor()));

            return dictionary;
        }
    }
}
