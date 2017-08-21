using System;
using System.Collections.Generic;
using System.Linq;
using General.CIR.Config;
using General.CIR.Models;

namespace General.CIR.Extentions
{
    public static class EngineNumberExtention
    {
        public static string ToStrNumber(this string value)
        {
            bool flag = string.IsNullOrEmpty(value) || value.Length != 8;
            if (flag)
            {
                throw new ArgumentException(string.Format("{0} is not null or value's length != 8", "value"));
            }
            string text = value.Substring(0, 3);
            string arg = GlobalParams.Instance.AllEngineTypes.ContainsKey(int.Parse(text)) ? GlobalParams.Instance.AllEngineTypes[Convert.ToInt32(text)].StrNumber : text;
            return string.Format("{0}-{1}", arg, value.Substring(3, 5));
        }

        public static string ToNumber(this string value)
        {
            bool flag = string.IsNullOrEmpty(value);
            if (flag)
            {
                throw new ArgumentNullException(string.Format("{0} 不能为null 或者空字符", "value"));
            }
            string[] tmp = value.Split(new char[]
            {
                '-'
            });
            bool flag2 = tmp.Length != 2;
            if (flag2)
            {
                throw new Exception("不是指定格式的机车号");
            }
            IEnumerable<KeyValuePair<int, EngineType>> arg_88_0 = GlobalParams.Instance.AllEngineTypes;

            using (IEnumerator<KeyValuePair<int, EngineType>> enumerator = arg_88_0.Where(t => t.Value.StrNumber.Equals((tmp[0]))).GetEnumerator())
            {
                if (enumerator.MoveNext())
                {
                    KeyValuePair<int, EngineType> current = enumerator.Current;
                    return current.Value.Number + tmp[0];
                }
            }
            throw new Exception("机车号错误");
        }
    }
}
