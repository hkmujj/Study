using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CBTC.DataAdapter.Util
{
    public interface IStringFloatConverter
    {
        char[] ConvertFloatArray(float[] data);

        float[] ConvertCharArray(char[] data);

        float GetNumberInt(string str);
    }

    class StringFloatConverter : IStringFloatConverter
    {
        public char[] ConvertFloatArray(float[] data)
        {
            return
                data.SelectMany(BitConverter.GetBytes).Select(s => BitConverter.ToChar(new byte[] {s, 0}, 0)).ToArray();
        }

        public float[] ConvertCharArray(char[] data)
        {
            var ba = data.Select(s => BitConverter.GetBytes(s)[0]).ToArray();
            var rlt = new List<float>(ba.Length/4);
            for (int i = 0; i < ba.Length; i += 4)
            {
                rlt.Add(BitConverter.ToSingle(ba, i));
            }

            return rlt.ToArray();
        }

        public float GetNumberInt(string str)
        {
            var number = new StringBuilder();
            if (!string.IsNullOrEmpty(str))
            {
                foreach (var item in str.Where(w => w >= '0' && w <= '9'))
                {
                    number.Append(item);
                }
            }

            return float.Parse(number.ToString());
        }
    }
}